using Microsoft.AspNetCore.Components;
using Reach.EditorApp.ServiceModel;
using Reach.Membership.Views;
using Reach.Orchestration.ServiceModel;

namespace Reach.EditorApp.Client.Pages;

public partial class Home : ComponentBase
{
    private readonly ITenantService _tenantService;
    private readonly NavigationManager _navigation;
    private readonly IRegionUrlFormatter _regionUrlFormatter;

    private IEnumerable<AvailableTenantView> _tenants = [];

    public Home(ITenantService tenantService, NavigationManager navigation, IRegionUrlFormatter regionUrlFormatter)
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
