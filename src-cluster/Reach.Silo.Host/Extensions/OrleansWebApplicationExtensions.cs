using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Reach.Cqrs;
using Reach.Membership;
using System.Reflection;

namespace Reach.Silo.Host.Extensions;

/// <summary>
/// Custom extensions for the Web Application hosting the Orleans Cluster
/// </summary>
public static class OrleansWebApplicationExtensions
{
    /// <summary>
    /// Maps a Grain's command interface methods as endpoints
    /// </summary>
    /// <typeparam name="TGrain">The type of grain to support</typeparam>
    /// <param name="application">The web application</param>
    /// <param name="route">The route path for the particular grain. The route is added into the string /api/{route}/{aggregateId}/execute</param>
    /// <param name="configure">An optional method for configuring the endpoint. By default, all matching methods will be added</param>
    /// <returns>The original web application</returns>
    public static WebApplication MapGrainEndpoint<TGrain>(
        this WebApplication application,
        string route,
        Action<GrainEndpointConfig<TGrain>>? configure = null)
        where TGrain : class, IGrainWithGuidCompoundKey
    {
        var config = new GrainEndpointConfig<TGrain>();

        var methods = typeof(TGrain).GetMethods(BindingFlags.Instance | BindingFlags.Public);
        
        foreach (var method in methods)
        {
            if(method.ReturnType == typeof(Task<CommandResponse>))
            {
                config.Add(method.GetParameters().First().ParameterType, method);
            }
        }

        if (configure is not null)
        {
            configure(config);
        }

        var sanitizedRoute = route.TrimStart('/').TrimEnd('/').Trim();
        application.MapPost(
            $"/api/{sanitizedRoute}/{{aggregateId}}/execute",
            async (
                [FromRoute] Guid aggregateId, 
                [FromServices] IClusterClient clusterClient, 
                [FromHeader(Name = "X-Command-Type")]string commandTypeHeader,
                [FromHeader(Name = TenantHttpConstants.TenantIdHeader)]string tenantIdHeader,
                HttpRequest request
            ) =>
            {
                if(string.IsNullOrWhiteSpace(tenantIdHeader) || !Guid.TryParse(tenantIdHeader, out Guid tenantId))
                {
                    throw new UnauthorizedAccessException();
                }

                // TODO: Validate the user can access this tenant

                // get access to the grain
                var grain = clusterClient.GetGrain<TGrain>(aggregateId, tenantIdHeader.ToString());
                var commandType = Type.GetType(commandTypeHeader)!;

                string body = "";
                using (StreamReader stream = new StreamReader(request.Body))
                {
                    body = await stream.ReadToEndAsync();
                }

                // deserialize the command
                var command = JsonConvert.DeserializeObject(body, commandType);

                // force the tenant id into the command to avoid chance
                // user submits with tenant header but substitutes another 
                // tenant in the command body
                if (command is AggregateCommand aggregateCommand)
                {
                    aggregateCommand.TenantId = tenantId;
                }

                // route the command to the grain
                var map = config.Maps.First(m => m.CommandType == commandType);
                var response = (Task<CommandResponse>)map.Method.Invoke(grain, [command])!;

                return await response;
            }
        );

        return application;
    }
}
