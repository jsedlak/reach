using Microsoft.Extensions.Logging;
using Reach.Content.Commands.FieldDefinitions;
using Reach.Content.Views;
using Reach.Cqrs;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Services;

public class HttpFieldDefinitionService : BaseContentService, IFieldDefinitionService
{
    private const string Endpoint = "field-definitions";

    public HttpFieldDefinitionService(IGraphClient graphClient, ICommandClient commandClient, ILogger<HttpFieldDefinitionService> logger)
        : base(graphClient, commandClient, logger)
    {
    }

    public async Task<CommandResponse> Create(CreateFieldDefinitionCommand command)
    {
        return await CommandClient.Execute(Endpoint, command);
    }

    public async Task<IEnumerable<FieldDefinitionView>> GetFieldDefinitons(Guid organizationId, Guid hubId, string? query = null)
    {
        return await GraphClient.GetMany<FieldDefinitionView>(organizationId, hubId, query: query);
    }
}
