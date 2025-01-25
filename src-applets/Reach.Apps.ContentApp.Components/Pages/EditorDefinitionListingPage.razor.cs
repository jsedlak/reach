using Microsoft.AspNetCore.Components;
using Reach.Content.Commands.EditorDefinitions;
using Reach.Content.Model;
using Reach.Content.Views;
using Reach.Platform.ServiceModel;
using Reach.Security;
using Tazor.Components.Layout;

namespace Reach.Apps.ContentApp.Components.Pages;

[TenantRequired]
public partial class EditorDefinitionListingPage : ContentBasePage
{
    private IEnumerable<EditorDefinitionView> _editorDefinitions = [];

    private DialogContext<CreateEditorDefinitionCommand> _createContext = new(() => { });

    private EditorDefinitionView? _editContext = new();
    private AddParameterToEditorDefinitionCommand _addParameterCommand = new();
    private bool _isEditFlyoutVisible = false;

    public EditorDefinitionListingPage(IEditorDefinitionService editorDefinitionService, IServiceProvider serviceProvider)
    {
        EditorDefinitionService = editorDefinitionService;

        _createContext = new(StateHasChanged);
    }

    #region Data Setup
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
        if (CurrentOrganization is not null && CurrentHub is not null)
        {
            _editorDefinitions =
                await EditorDefinitionService.GetEditorDefinitions(CurrentOrganization.Id, CurrentHub.Id);
        }
    }
    #endregion

    #region Creation
    private Task OnBeginCreateClicked()
    {
        return _createContext.Open(
            new CreateEditorDefinitionCommand(
                CurrentOrganization!.Id, 
                CurrentHub!.Id,
                Guid.NewGuid()
            )
        );
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
    #endregion

    #region Editing
    private void OnBeginEditClicked(EditorDefinitionView model)
    {
        _editContext = model;
        _isEditFlyoutVisible = true;
        _addParameterCommand = new();
        StateHasChanged();
    }

    private void OnNewParameterTypeChanged(ChangeEventArgs e)
    {
        if(e.Value is null)
        {
            return;
        }

        if(Enum.TryParse(typeof(EditorParameterType), e.Value.ToString() ?? "Text", out var type))
        {
            _addParameterCommand.Type = (EditorParameterType)type;
        }
    }

    private async Task CompleteAddParameter()
    {
        // EditorDefinitionService.AddParameter(_addParameterCommand);
        
        
    }
    #endregion

    private IEditorDefinitionService EditorDefinitionService { get; }
    
}
