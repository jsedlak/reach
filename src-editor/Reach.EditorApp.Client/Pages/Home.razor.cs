using Microsoft.AspNetCore.Components;
using Reach.EditorApp.Client.Services;
using Reach.Membership.Views;
using Reach.Orchestration.ServiceModel;

namespace Reach.EditorApp.Client.Pages;

public partial class Home : ComponentBase
{
    private readonly HttpTenantService _tenantService;
    private readonly NavigationManager _navigation;
    private readonly IRegionUrlFormatter _regionUrlFormatter;

    private IEnumerable<AvailableTenantView> _tenants = [];

    public Home(HttpTenantService tenantService, NavigationManager navigation, IRegionUrlFormatter regionUrlFormatter)
    {
        _tenantService = tenantService;
        _navigation = navigation;
        _regionUrlFormatter = regionUrlFormatter;
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
