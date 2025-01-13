using Reach.Apps.ContentApp.Services;
using Reach.Content.Commands.Components;
using Reach.Content.Views;
using Reach.Security;
using Tazor.Components.Layout;

namespace Reach.Apps.ContentApp.Components.Pages;

[TenantRequired]
public partial class ComponentListingPage : ContentBasePage
{
    private readonly ComponentService _componentService;
    private IEnumerable<ComponentView> _components;

    private DialogContext<CreateComponentCommand> _createContext = new(() => { });

    public ComponentListingPage(ComponentService componentService)
    {
        _componentService = componentService;
        
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
            _components = await _componentService.GetComponents(CurrentOrganization.Id, CurrentHub.Id);
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

            var result = await _componentService.Create(model);

            return (result.IsSuccess, []);
        });
    }
}
