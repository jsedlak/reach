using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Pages;

public partial class Home : ComponentBase
{
    private readonly NavigationManager _navigation;
    private readonly IMembershipService _membershipService;

    public Home(NavigationManager navigation, IMembershipService membershipService)
    {
        _navigation = navigation;
        _membershipService = membershipService;
    }

    protected override async Task OnInitializedAsync()
    {
        var organizations = await _membershipService.GetAvailableOrganizations();

        if (!organizations.Any())
        {
            _navigation.NavigateTo("/onboarding");
        }
    }

    [CascadingParameter]
    public AuthenticationState AuthenticationState { get; set; }
}
