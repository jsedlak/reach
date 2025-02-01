using Reach.Content.Views;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Providers;

internal class ContentContextProvider : BaseContextProvider, IContentContextProvider
{
    private readonly IEditorDefinitionService _editorDefinitionService;

    public ContentContextProvider(IEditorDefinitionService editorDefinitionService)
    {
        _editorDefinitionService = editorDefinitionService;
    }

    public override async Task Refresh(Guid organizationId, Guid hubId)
    {
        EditorDefinitions = await _editorDefinitionService.GetEditorDefinitions(
            organizationId, 
            hubId
        );

        OnContextChanged();
    }

    public IEnumerable<EditorDefinitionView> EditorDefinitions { get; private set; } = [];
}