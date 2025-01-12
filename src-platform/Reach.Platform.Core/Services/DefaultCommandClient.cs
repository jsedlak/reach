using System.Net.Http.Json;
using System.Text.Json;
using Reach.Cqrs;
using Reach.Membership;
using Reach.Platform.Http;
using Reach.Platform.Json;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Services;

internal sealed class DefaultCommandClient : ICommandClient
{
    private readonly HttpClient _httpClient;
    
    public DefaultCommandClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("api");
    }
    
    public async Task<CommandResponse> Execute<TCommand>(string endpoint, TCommand command)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(command, DefaultJsonOptions.Instance), 
            HttpConstants.ApplicationJsonMediaType
        );
        
        // if we have an identifier...
        var entityIdentifier = string.Empty;

        // add our custom headers
        content.Headers.Add("X-Command-Type", typeof(TCommand).AssemblyQualifiedName);
        if (command is AggregateCommand aggregateCommand)
        {
            content.Headers.Add(MembershipHttpConstants.OrganizationIdHeader, aggregateCommand.OrganizationId.ToString());
            content.Headers.Add(MembershipHttpConstants.HubIdHeader, aggregateCommand.HubId.ToString());

            entityIdentifier = aggregateCommand.AggregateId.ToString();
        }
        
        // we need to add a path marker if entityIdentifier is not empty
        if (!string.IsNullOrWhiteSpace(entityIdentifier))
        {
            entityIdentifier += "/";
        }

        // call the api
        var response = await _httpClient.PostAsync(
            $"api/{endpoint}/{entityIdentifier}execute",
            content
        );

        return await response.Content.ReadFromJsonAsync<CommandResponse>(DefaultJsonOptions.Instance) ??
               new CommandResponse();
    }
}