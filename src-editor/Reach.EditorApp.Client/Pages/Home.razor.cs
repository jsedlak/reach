using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.Components.Context;
using Reach.EditorApp.ServiceModel;
using Reach.Membership.ServiceModel;
using Reach.Membership.Views;
using Reach.Orchestration.ServiceModel;

namespace Reach.EditorApp.Client.Pages;

public partial class Home : ComponentBase
{
    private readonly IOrganizationService _organizationService;
    private readonly NavigationManager _navigation;
    private readonly IRegionUrlFormatter _regionUrlFormatter;

    private IEnumerable<AvailableOrganizationView> _organizations = [];

    private bool _isLoadingTenants = false;

    public Home(IOrganizationService organizationService, NavigationManager navigation, IRegionUrlFormatter regionUrlFormatter)
    {
        _organizationService = organizationService;
        _navigation = navigation;
        _regionUrlFormatter = regionUrlFormatter;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (AuthenticationState == null)
        {
            Console.WriteLine("AuthenticationState is null");
            return;
        }
        
        var userIsAuthenticated = AuthenticationState.User.Identity != null &&
            AuthenticationState.User.Identity.IsAuthenticated &&
            AuthenticationState.User.Identity.Name != null;

        if(firstRender && userIsAuthenticated)
        {
            _isLoadingTenants = true;
            StateHasChanged();

            _organizations = await _organizationService.GetOrganizationsForUserAsync(AuthenticationState.User.Identity!.Name!);

            if (!_organizations.Any())
            {
                _navigation.NavigateTo("/onboarding");
            }

            _isLoadingTenants = false;
            StateHasChanged();
        }
    }

    [CascadingParameter]
    public AuthenticationState AuthenticationState { get; set; }

}
