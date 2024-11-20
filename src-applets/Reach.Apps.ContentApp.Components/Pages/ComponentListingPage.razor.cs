using Microsoft.AspNetCore.Components.Authorization;
using Reach.Security;

namespace Reach.Apps.ContentApp.Components.Pages;

[TenantRequired]
public partial class ComponentListingPage : ContentBasePage
{
    public ComponentListingPage()
    {
    }
}
