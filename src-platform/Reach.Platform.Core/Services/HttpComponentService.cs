using Microsoft.Extensions.Logging;
using Reach.Content.Commands.Components;
using Reach.Content.Views;
using Reach.Cqrs;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Services;

public class HttpComponentService : BaseContentService, IComponentService
{
    private const string Endpoint = "components";

    public HttpComponentService(IGraphClient graphClient, ICommandClient commandClient, ILogger<IComponentService> logger)
        : base(graphClient, commandClient, logger)
    {
    }

    public async Task<CommandResponse> Create(CreateComponentCommand command)
    {
        return await CommandClient.Execute(Endpoint, command);
    }

    public async Task<CommandResponse> SetFieldValue(SetComponentFieldValueCommand command)
    {
        return await CommandClient.Execute(Endpoint, command);
    }

    public async Task<IEnumerable<ComponentView>> GetComponents(Guid organizationId, Guid hubId, string? query = null)
    {
        return await GraphClient.GetMany<ComponentView>(organizationId, hubId, query: query);
    }
}