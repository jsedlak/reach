using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.Components;

namespace Reach.Apps.ContentApp.Components.Pages;

public abstract class ContentBasePage : BaseTenantComponent
{
    [Inject]
    protected AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
}
