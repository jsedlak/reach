using Microsoft.Extensions.Logging;
using Reach.Content.Commands.EditorDefinitions;
using Reach.Content.Views;
using Reach.Cqrs;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Services;

public class HttpEditorDefinitionService : BaseContentService, IEditorDefinitionService
{
    private const string Endpoint = "editor-definitions";

    public HttpEditorDefinitionService(IGraphClient graphClient, ICommandClient commandClient, ILogger<HttpEditorDefinitionService> logger)
        : base(graphClient, commandClient, logger)
    {
    }

    public async Task<CommandResponse> Create(CreateEditorDefinitionCommand command)
    {
        return await CommandClient.Execute(Endpoint, command);
    }

    public async Task<CommandResponse> SetEditorDefinitionParameters(SetEditorDefinitionParametersCommand command)
    {
        return await CommandClient.Execute(Endpoint, command);
    }

    public async Task<IEnumerable<EditorDefinitionView>> GetEditorDefinitions(Guid organizationId, Guid hubId, string? query = null)
    {
        return await GraphClient.GetMany<EditorDefinitionView>(organizationId, hubId, query: query);
    }


}