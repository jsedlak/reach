using Microsoft.AspNetCore.Components;
using Reach.Content.Views;
using Reach.Platform.ServiceModel;
using Tazor.Components.Theming;

namespace Reach.Platform.Components.Content;

public partial class EditorSelector : BaseReachComponent
{
    private readonly IThemeManager _themeManager;
    private readonly IEditorService _editorService;
    private IEnumerable<EditorView> _editors = [];

    public EditorSelector(IThemeManager themeManager, IEditorService editorService)
    {
        _themeManager = themeManager;
        _editorService = editorService;
    }

    private async Task OnEditorChanged(ChangeEventArgs args)
    {
        SelectedEditor = args.Value?.ToString() ?? "";
        await SelectedEditorChanged.InvokeAsync(SelectedEditor);
    }

    protected override async Task OnInitializedAsync()
    {
        _editors = await _editorService.GetEditors() ?? [];
    }

    [Parameter]
    public string SelectedEditor { get; set; }

    [Parameter]
    public EventCallback<string> SelectedEditorChanged { get; set; }
}
