using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.Membership.Views;
using Reach.Platform.Layout;
using Reach.Platform.Providers;

namespace Reach.Platform.Pages;

public partial class Home : ComponentBase
{
    private readonly NavigationManager _navigationManager;
    private readonly IContentContextProvider _contentContextProvider;

    private bool _isCreateHubDialogVisible;

    public Home(NavigationManager navigationManager, IContentContextProvider contentContextProvider)
    {
        _navigationManager = navigationManager;
        _contentContextProvider = contentContextProvider;
    }

    protected override void OnParametersSet()
    {

        Console.WriteLine("Parameters set in Home");
        
        if (Organizations is not null)
        {
            Console.WriteLine($"Hubs: {Organizations.Sum(m => m.Hubs.Count())}");
        }

        base.OnParametersSet();
    }

    [CascadingParameter(Name = "Layout")]
    public GlobalLayout? Layout { get; set; }

    /// <summary>
    /// Gets or Sets the authentication state
    /// </summary>
    [CascadingParameter]
    public AuthenticationState? AuthenticationState { get; set; }
    
    /// <summary>
    /// Gets or Sets the set of organizations the user has access to
    /// </summary>
    [CascadingParameter(Name = "Organizations")]
    public IEnumerable<AvailableOrganizationView>? Organizations { get; set; }
}
