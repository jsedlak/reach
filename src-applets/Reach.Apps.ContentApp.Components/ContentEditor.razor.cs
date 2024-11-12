using Microsoft.AspNetCore.Components;
using Reach.Apps.ContentApp.Services;
using Reach.Content.Views;

namespace Reach.Apps.ContentApp.Components;

public partial class ContentEditor : ComponentBase
{
    private EditorDefinitionService _editorDefinitionService;
    private IEnumerable<EditorDefinitionView> _editorDefinitions = [];

    protected override async Task OnInitializedAsync()
    {
        _editorDefinitionService = new EditorDefinitionService(ServiceProvider);
        _editorDefinitions = await _editorDefinitionService.GetEditorDefinitions();

        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        

        await base.OnAfterRenderAsync(firstRender);
    }

    [Inject]
    protected IServiceProvider ServiceProvider { get;set; } = null!;
}
