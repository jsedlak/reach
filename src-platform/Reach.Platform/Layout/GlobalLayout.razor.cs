﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.Platform.Providers;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Layout;

public partial class GlobalLayout : AuthenticatedLayoutBase
{
    private readonly IContentContextProvider _contentContextProvider;
    public GlobalLayout(NavigationManager navigation, 
        IMembershipService membershipService, 
        IOrganizationService organizationService, 
        AuthenticationStateProvider authenticationStateProvider,
        IContentContextProvider contentContextProvider) 
        : base(navigation, membershipService, organizationService, authenticationStateProvider)
    {
        _contentContextProvider = contentContextProvider;
        _contentContextProvider.ContextChanged += (sender, args) => StateHasChanged();
    }

    protected override async Task OnPageReady()
    {
        await _contentContextProvider.Refresh(
            CurrentOrganizationId.GetValueOrDefault(),
            CurrentHubId.GetValueOrDefault()
        );
    }
}
