using Microsoft.AspNetCore.Components;
using Reach.Apps.ContentApp.Services;
using Reach.Components.Context;
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
        _components = await _componentService.GetComponents();
    }
    
    private Task OnBeginCreateClicked()
    {
        return _createContext.Open(new (Guid.Empty, Guid.Empty, Guid.NewGuid()));
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
    
    [CascadingParameter]
    public TenantContext TenantContext { get; set; } = null!;
}
