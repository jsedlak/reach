﻿using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.Membership.Views;
using Reach.Platform.ServiceModel;

namespace Reach.EditorApp.Client.Pages;

public partial class Home : ComponentBase
{
    private readonly IOrganizationService _organizationService;
    private readonly NavigationManager _navigation;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    private IEnumerable<AvailableOrganizationView> _organizations = [];

    private bool _isLoadingTenants = false;

    public Home(
        IOrganizationService organizationService, 
        NavigationManager navigation,
        AuthenticationStateProvider authenticationStateProvider)
    {
        _organizationService = organizationService;
        _navigation = navigation;
        _authenticationStateProvider = authenticationStateProvider;
    }

    protected override async Task OnInitializedAsync()
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        if (state.User.Identity is { IsAuthenticated: true })
        {
            await OnUserAuthenticated(state.User);
        }
    }

    protected virtual async Task OnUserAuthenticated(ClaimsPrincipal principal)
    {
        
        Console.WriteLine(nameof(OnUserAuthenticated));
        _isLoadingTenants = true;
        StateHasChanged();

        _organizations = await _organizationService.GetAvailableOrganizations();
        
        if (!_organizations.Any())
        {
            _navigation.NavigateTo("/onboarding");
        }
        
        _isLoadingTenants = false;
        StateHasChanged();
    }

}
