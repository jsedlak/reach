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


    public EditorDefinitionService(IServiceProvider serviceProvider)
        : base(serviceProvider)
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
