using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Reach.Content.Views;
using Reach.Silo.Agents.PluginModel;
using Reach.Silo.Content.ServiceModel;
using System.ComponentModel;

namespace Reach.Silo.Agents.Plugins;

public class EditorDefinitionPlugin
{
    private IEditorDefinitionViewReadRepository _editorDefinitionViewReadRepository;
    private ILogger<EditorDefinitionPlugin> _logger;
    public EditorDefinitionPlugin(
        ILogger<EditorDefinitionPlugin> logger,
        IEditorDefinitionViewReadRepository editorDefinitionViewReadRepository)
    {
        _logger = logger;
        _editorDefinitionViewReadRepository = editorDefinitionViewReadRepository;
    }

    [KernelFunction("get_editor_definitions")]
    [Description("Gets a list of all the editor definitions and their parameters")]
    public async Task<EditorDefinitionView[]> GetEditorDefinitions(TenancyModel model)
    {
        var result = await _editorDefinitionViewReadRepository.Query(
            model.OrganizationId,
            model.HubId
        );

        return result.ToArray();
    }
}
