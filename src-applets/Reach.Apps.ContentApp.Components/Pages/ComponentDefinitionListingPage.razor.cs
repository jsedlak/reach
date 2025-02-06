using Reach.Content.Commands.ComponentDefinitions;
using Reach.Content.Views;
using Reach.Platform.Providers;
using Reach.Platform.ServiceModel;
using Tazor.Components.Layout;

namespace Reach.Apps.ContentApp.Components.Pages;

public partial class ComponentDefinitionListingPage : ContentBasePage
{
    private readonly IContentContextProvider _contentContextProvider;
    private readonly IComponentDefinitionService _componentDefinitionService;

    private DialogContext<CreateComponentDefinitionCommand> _createContext = new(() => { });

    private ComponentDefinitionView _editContext = new();
    private bool _isEditFlyoutVisible = false;

    private AddFieldToComponentDefinitionCommand _addFieldCommand = new();

    public ComponentDefinitionListingPage(
        IContentContextProvider contentContextProvider,
        IComponentDefinitionService componentDefinitionService)
    {
        _contentContextProvider = contentContextProvider;
        _componentDefinitionService = componentDefinitionService;
        
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
        if (CurrentOrganization is not null && CurrentHub is not null)
        {
            await _contentContextProvider.Refresh(
                CurrentOrganizationId.GetValueOrDefault(),
                CurrentHubId.GetValueOrDefault()
            );
        }
    }

    #region Creating
    private Task OnBeginCreateClicked()
    {
        return _createContext.Open(new (
            CurrentOrganization!.Id,
            CurrentHub!.Id, 
            Guid.NewGuid()
        ));
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

            var result = await _componentDefinitionService.Create(model);

            return (result.IsSuccess, []);
        });
    }
    #endregion

    #region Editing
    private void BeginEdit(ComponentDefinitionView model)
    {
        _addFieldCommand = new(
            CurrentOrganizationId.GetValueOrDefault(),
            CurrentHubId.GetValueOrDefault(),
            model.Id
        );

        _editContext = model;
        _isEditFlyoutVisible = true; 
        StateHasChanged();
    }

    private async Task OnFieldAdded()
    {
        await _componentDefinitionService
            .AddFieldToComponentDefinition(_addFieldCommand);

        await RefreshData();

        _editContext = _contentContextProvider.ComponentDefinitions
            .First(m => m.Id == _editContext.Id);

        _addFieldCommand = new(
            CurrentOrganizationId.GetValueOrDefault(),
            CurrentHubId.GetValueOrDefault(),
            _editContext.Id
        );
    }
    #endregion
}