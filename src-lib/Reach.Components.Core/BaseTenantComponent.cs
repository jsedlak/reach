using Microsoft.AspNetCore.Components;

namespace Reach.Components;

public abstract class BaseTenantComponent : ComponentBase
{
    [Parameter]
    public string OrganizationSlug { get; set; } = null!;

    [Parameter]
    public string HubSlug { get; set; } = null!;
}
