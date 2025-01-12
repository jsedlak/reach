using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.WebUtilities;
using Reach.Membership.Views;
using Reach.Platform.ServiceModel;
using Reach.Security;
using System;

namespace Reach.Platform.Layout;

public partial class GlobalLayout : LayoutComponentBase
{
    private IEnumerable<AvailableOrganizationView> _organizations = [];

    private readonly NavigationManager _navigation;
    private readonly IMembershipService _membershipService;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public GlobalLayout(
        NavigationManager navigation, 
        IMembershipService membershipService,
        AuthenticationStateProvider authenticationStateProvider)
    {
        _navigation = navigation;
        _membershipService = membershipService;
        _authenticationStateProvider = authenticationStateProvider;

        _authenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;
    }

    private async Task InitializeOrganizations()
    {
        var settings = await _membershipService.GetAccountSettings();
        _organizations = await _membershipService.GetAvailableOrganizations();

        var uri = _navigation.ToAbsoluteUri(_navigation.Uri);
        var queryStringParams = QueryHelpers.ParseQuery(uri.Query);

        var skip = settings.SkipsOnboarding ||
            queryStringParams.ContainsKey("skipOnboarding");

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
