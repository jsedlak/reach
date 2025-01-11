using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Reach.Membership.Views;
using Reach.Platform.ServiceModel;
using System;

namespace Reach.Platform.Layout;

public partial class GlobalLayout : LayoutComponentBase
{
    private IEnumerable<AvailableOrganizationView> _organizations = [];

    private readonly NavigationManager _navigation;
    private readonly IMembershipService _membershipService;

    public GlobalLayout(NavigationManager navigation, IMembershipService membershipService)
    {
        _navigation = navigation;
        _membershipService = membershipService;
    }

    protected override async Task OnInitializedAsync()
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
}
