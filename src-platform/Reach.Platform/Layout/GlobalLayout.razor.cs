using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Layout;

public partial class GlobalLayout : AuthenticatedLayoutBase
{
    public GlobalLayout(NavigationManager navigation, IMembershipService membershipService, IOrganizationService organizationService, AuthenticationStateProvider authenticationStateProvider) 
        : base(navigation, membershipService, organizationService, authenticationStateProvider)
    {
    }
}
