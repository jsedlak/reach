using Microsoft.AspNetCore.Components;
using Reach.Content.Commands.EditorDefinitions;
using Reach.Content.Model;
using Reach.Content.Views;
using Reach.Platform.ServiceModel;
using Reach.Security;
using Tazor.Components.Layout;
using Tazor.Extensions;

namespace Reach.Apps.ContentApp.Components.Pages;

[TenantRequired]
public partial class EditorDefinitionListingPage : ContentBasePage
{
    private IEnumerable<EditorDefinitionView> _editorDefinitions = [];

    private DialogContext<CreateEditorDefinitionCommand> _createContext = new(() => { });

    private EditorDefinitionView? _editContext = new();
    private EditorParameterDefinition _editContextParameter = new();
    private IEnumerable<EditorParameterDefinition> _editParameters = [];
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
        _editContextParameter = new();
        _editParameters = model.Parameters.ToArray();
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
            _editContextParameter.Type = (EditorParameterType)type;
        }
    }

    private async Task OnRemoveParameter(EditorParameterDefinition parameter)
    {
        _editParameters = _editParameters
            .Where(m => m != parameter)
            .ToArray();

        StateHasChanged();
    }

    private async Task OnAddParameter()
    {
        _editContextParameter.Name = _editContextParameter.DisplayName.ToSlug();
        _editContextParameter.Description = "";

        _editParameters = [.. _editParameters, _editContextParameter];
        _editContextParameter = new();
        StateHasChanged();
    }

    private async Task OnSaveParameters()
    {
        await EditorDefinitionService.SetEditorDefinitionParameters(
            new SetEditorDefinitionParametersCommand(
            CurrentOrganizationId.GetValueOrDefault(), 
            CurrentHubId.GetValueOrDefault(),
            _editContext!.Id)
            {
                Parameters = _editParameters.ToArray()
            }
        );

        _editContext = new();
        _editContextParameter = new();
        _editParameters = [];

        _isEditFlyoutVisible = false;

        await RefreshData();
        
        StateHasChanged();
    }
    #endregion

    private IEditorDefinitionService EditorDefinitionService { get; }
    
}
