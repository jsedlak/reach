using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Reach.Components.Context;
using Reach.Content.Commands.Editors;
using Reach.Content.Views;
using Reach.Cqrs;

namespace Reach.Apps.ContentApp.Services;

public class EditorDefinitionService : BaseService
{
    private const string DefaultQuery = @"
    query {
        editorDefinitions {
            id
            name
            editorType
            parameters {
              name
              displayName
              description
              type
            }
        }
    }
    ";

    public EditorDefinitionService(ITenantContext tenantContext, AuthenticationStateProvider authenticationStateProvider, IHttpClientFactory httpClientFactory, ILogger<EditorDefinitionService> logger)
        : base(tenantContext, authenticationStateProvider, httpClientFactory, logger)
    {
    }

    public async Task<CommandResponse> Create(CreateEditorDefinitionCommand command)
    {
        return await ExecuteCommandAsync("editor-definitions", command);
    }

    public async Task<IEnumerable<EditorDefinitionView>> GetEditorDefinitions(string? query = null)
    {
        return await QueryGraphAsync<EditorDefinitionView>("editorDefinitions", query ?? DefaultQuery);
    }
}
