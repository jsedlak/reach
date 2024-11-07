using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Reach.Cqrs;
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
        where TGrain : class, IGrainWithGuidKey
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
            async ([FromRoute] Guid aggregateId, 
            [FromServices] IClusterClient clusterClient, 
            [FromHeader(Name = "X-Command-Type")]string commandTypeHeader,
            HttpRequest request) =>
            {
                var grain = clusterClient.GetGrain<TGrain>(aggregateId);
                var commandType = Type.GetType(commandTypeHeader)!;

                string body = "";
                using (StreamReader stream = new StreamReader(request.Body))
                {
                    body = await stream.ReadToEndAsync();
                }

                var command = JsonConvert.DeserializeObject(body, commandType);

                var map = config.Maps.First(m => m.CommandType == commandType);

                var response = (Task<CommandResponse>)map.Method.Invoke(grain, [command])!;
                return await response;
            }
        );

        return application;
    }
}
