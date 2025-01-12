using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Reach.Membership.Views;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Layout;

public partial class GlobalLayout : LayoutComponentBase
{
    private IEnumerable<AvailableOrganizationView>? _organizations = null;

    private readonly NavigationManager _navigation;
    private readonly IMembershipService _membershipService;
    private readonly IOrganizationService _organizationService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    private bool _isLoading = false;

    public GlobalLayout(
        NavigationManager navigation, 
        IMembershipService membershipService,
        IOrganizationService organizationService,
        AuthenticationStateProvider authenticationStateProvider)
    {
        _navigation = navigation;
        _membershipService = membershipService;
        _organizationService = organizationService;
        _authenticationStateProvider = authenticationStateProvider;

        _authenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;
    }

    private async Task InitializeOrganizations()
    {
        _isLoading = true;
        StateHasChanged();

        var settings = await _membershipService.GetAccountSettings();
        _organizations = await _organizationService.GetAvailableOrganizations();

        var uri = _navigation.ToAbsoluteUri(_navigation.Uri);
        var queryStringParams = QueryHelpers.ParseQuery(uri.Query);

        var skip = settings.SkipsOnboarding ||
            queryStringParams.ContainsKey("skipOnboarding");

        _isLoading = false;
        StateHasChanged();

        if (!_organizations.Any())
        {
            if (settings.SkipsOnboarding || skip)
            {
                return;
            }

            _navigation.NavigateTo("/onboarding");
        }
    }

    private async void OnAuthenticationStateChanged(Task<AuthenticationState> task)
    {
        var state = await task;

        if(state is not null && state.User.Identity is {  IsAuthenticated: true })
        {
            Console.WriteLine("OnAuthenticationStateChanged -> Initializing Orgs");
            await InitializeOrganizations();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine("GlobalLayout::OnInitializedAsync");

        if (AuthenticationState is null)
        {
            Console.WriteLine("Auth state is null. Waiting.");
            return;
        }

        Console.WriteLine("OnInitializedAsync -> Initializing Orgs");
        await InitializeOrganizations();
    }

    [CascadingParameter]
    public AuthenticationState AuthenticationState { get; set; }
}
