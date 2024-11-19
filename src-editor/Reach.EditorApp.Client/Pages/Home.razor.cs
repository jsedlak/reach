using Microsoft.AspNetCore.Components;
using Reach.EditorApp.Client.Services;
using Reach.Membership.Views;

namespace Reach.EditorApp.Client.Pages;

public partial class Home : ComponentBase
{
    private readonly HttpTenantService _tenantService;
    private readonly NavigationManager _navigation;

    private IEnumerable<AvailableTenantView> _tenants = [];

    public Home(HttpTenantService tenantService, NavigationManager navigation)
    {
        _tenantService = tenantService;
        _navigation = navigation;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if(firstRender)
        {
            _tenants = await _tenantService.GetTenantsForUserAsync();

            if (!_tenants.Any())
            {
                _navigation.NavigateTo("/onboarding");
            }

            StateHasChanged();
        }
    }
}
