using Microsoft.AspNetCore.Components;
using Reach.Content.Views;
using Reach.Platform.ServiceModel;
using Tazor.Components.Theming;

namespace Reach.Platform.Components.Content;

public partial class EditorDefinitionSelector : BaseReachComponent
{
    private readonly IThemeManager _themeManager;
    private readonly IEditorDefinitionService _editorDefinitionService;
    private IEnumerable<EditorDefinitionView> _editorDefinitions = [];

    public EditorDefinitionSelector(IThemeManager themeManager, IEditorDefinitionService editorDefinitionService)
    {
        _themeManager = themeManager;
        _editorDefinitionService = editorDefinitionService;
    }

    private async Task OnEditorDefinitionIdChanged(ChangeEventArgs args)
    {
        if(Guid.TryParse(args.Value?.ToString() ?? Guid.Empty.ToString(), out var newValue))
        {
            SelectedEditorDefinitionId = newValue;
            await SelectedEditorDefinitionIdChanged.InvokeAsync(SelectedEditorDefinitionId);
        }
    }

    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine(nameof(OnInitializedAsync) + $" IsReady={IsReady}");

        if (IsReady)
        {
            _editorDefinitions = await _editorDefinitionService.GetEditorDefinitions(
                CurrentOrganizationId.GetValueOrDefault(),
                CurrentHubId.GetValueOrDefault()
            );
        }
    }

    protected override async Task OnComponentIsReady()
    {
        Console.WriteLine(nameof(OnComponentIsReady));

        _editorDefinitions = await _editorDefinitionService.GetEditorDefinitions(
            CurrentOrganizationId.GetValueOrDefault(),
            CurrentHubId.GetValueOrDefault()
        );
    }

    [Parameter]
    public Guid SelectedEditorDefinitionId { get; set; }

    [Parameter]
    public EventCallback<Guid> SelectedEditorDefinitionIdChanged { get; set; }
}
