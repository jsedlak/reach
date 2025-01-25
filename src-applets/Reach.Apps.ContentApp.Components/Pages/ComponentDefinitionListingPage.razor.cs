using Reach.Content.Commands.ComponentDefinitions;
using Reach.Content.Views;
using Reach.Platform.ServiceModel;
using Reach.Platform.Services;
using Tazor.Components.Layout;

namespace Reach.Apps.ContentApp.Components.Pages;

public partial class ComponentDefinitionListingPage : ContentBasePage
{
    private readonly IComponentDefinitionService _componentDefinitionService;
    private IEnumerable<ComponentDefinitionView> _componentDefinitions = [];

    private DialogContext<CreateComponentDefinitionCommand> _createContext = new(() => { });

    private bool _isEditPaneOpen;

    public ComponentDefinitionListingPage(IComponentDefinitionService componentDefinitionService)
    {
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
            _componentDefinitions = await _componentDefinitionService.GetComponentDefinitions(
                CurrentOrganization.Id, 
                CurrentHub.Id
            ) ?? [];
        }
    }
    
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
}