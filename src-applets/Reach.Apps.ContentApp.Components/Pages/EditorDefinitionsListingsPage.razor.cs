using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.Apps.ContentApp.Services;
using Reach.Content.Commands.Editors;
using Reach.Content.Views;
using Tazor.Components.Layout;

namespace Reach.Apps.ContentApp.Components.Pages;

public partial class EditorDefinitionsListingsPage : ContentBasePage
{
    private IEnumerable<EditorDefinitionView> _editorDefinitions = [];
    private DialogContext<CreateEditorDefinitionCommand> _createContext = new(() => { });

    public EditorDefinitionsListingsPage()
    {
        _createContext = new(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await RefreshData();
            StateHasChanged();
        }
    }

    private async Task RefreshData()
    {
        _editorDefinitions = await EditorDefinitionService.GetEditorDefinitions();
    }

    private Task OnBeginCreateClicked()
    {
        return _createContext.Open(new CreateEditorDefinitionCommand(Guid.NewGuid()));
    }

    private Task OnCancelCreateClicked()
    {
        return _createContext.Cancel();
    }

    private async Task OnConfirmCreateClicked()
    {
        await _createContext.Confirm(async (model) =>
        {
            var result = await EditorDefinitionService.Create(model);

            return (result.IsSuccess, []);
        });
    }

    [Inject]
    protected EditorDefinitionService EditorDefinitionService { get; set; } = null!;

    [Inject]

    private AuthenticationStateProvider authenticationStateProvider { get; set; }
}
