using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.Membership.Views;

namespace Reach.Platform.Pages;

public partial class Home : ComponentBase
{
    private bool _isPanelOpen;

    private readonly NavigationManager _navigationManager;

    public Home(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    [CascadingParameter]
    public AuthenticationState? AuthenticationState { get; set; }
    
    [CascadingParameter(Name = "Organizations")]
    public IEnumerable<AvailableOrganizationView>? Organizations { get; set; }
}
