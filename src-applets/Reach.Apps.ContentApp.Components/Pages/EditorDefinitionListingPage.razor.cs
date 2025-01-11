using Microsoft.AspNetCore.Components;
using Reach.Apps.ContentApp.Services;
using Reach.Components.Context;
using Reach.Content.Commands.EditorDefinitions;
using Reach.Content.Views;
using Reach.Security;
using Tazor.Components.Layout;

namespace Reach.Apps.ContentApp.Components.Pages;

[TenantRequired]
public partial class EditorDefinitionListingPage : ContentBasePage
{
    private IEnumerable<EditorDefinitionView> _editorDefinitions = [];

    private DialogContext<CreateEditorDefinitionCommand> _createContext = new(() => { });

    public EditorDefinitionListingPage(EditorDefinitionService editorDefinitionService, IServiceProvider serviceProvider)
    {
        EditorDefinitionService = editorDefinitionService;

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
        return _createContext.Open(new CreateEditorDefinitionCommand(Guid.Empty, Guid.Empty, Guid.NewGuid()));
    }

    private Task OnCancelCreateClicked()
    {
        return _createContext.Cancel();
    }

    private async Task OnConfirmCreateClicked()
    {
        await _createContext.Confirm(async (model) =>
        {
            if(model == null)
            {
                return (false, ["Invalid data."]);
            }

            var result = await EditorDefinitionService.Create(model);

            return (result.IsSuccess, []);
        });
    }

    private EditorDefinitionService EditorDefinitionService { get; }

    [CascadingParameter]
    public TenantContext TenantContext { get; set; } = null!;
}
