using Reach.Content.Views;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Providers;

internal class ContentContextProvider : BaseContextProvider, IContentContextProvider
{
    private readonly IEditorDefinitionService _editorDefinitionService;
    private readonly IFieldDefinitionService _fieldDefinitionService;

    public ContentContextProvider(IEditorDefinitionService editorDefinitionService,
        IFieldDefinitionService fieldDefinitionService)
    {
        _editorDefinitionService = editorDefinitionService;
        _fieldDefinitionService = fieldDefinitionService;
    }

    public override async Task Refresh(Guid organizationId, Guid hubId)
    {
        EditorDefinitions = await _editorDefinitionService.GetEditorDefinitions(
            organizationId, 
            hubId
        );

        FieldDefinitions = await _fieldDefinitionService.GetFieldDefinitions(
            organizationId,
            hubId
        );

        OnContextChanged();
    }

    public IEnumerable<EditorDefinitionView> EditorDefinitions { get; private set; } = [];

    public IEnumerable<FieldDefinitionView> FieldDefinitions { get; private set; } = [];
}