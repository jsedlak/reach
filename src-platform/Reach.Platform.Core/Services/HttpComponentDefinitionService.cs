using Microsoft.Extensions.Logging;
using Reach.Content.Commands.ComponentDefinitions;
using Reach.Content.Views;
using Reach.Cqrs;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Services;

public class HttpComponentDefinitionService : BaseContentService, IComponentDefinitionService
{
    private const string Endpoint = "component-definitions";

    public HttpComponentDefinitionService(IGraphClient graphClient, ICommandClient commandClient, ILogger<HttpComponentDefinitionService> logger)
        : base(graphClient, commandClient, logger)
    {
    }

    public async Task<CommandResponse> Create(CreateComponentDefinitionCommand command)
    {
        return await CommandClient.Execute(Endpoint, command);
    }

    public async Task<IEnumerable<ComponentDefinitionView>> GetComponentDefinitions(
        Guid organizationId, Guid hubId, string? query = null)
    {
        return await GraphClient.GetMany<ComponentDefinitionView>(organizationId, hubId, query: query);
    }
}