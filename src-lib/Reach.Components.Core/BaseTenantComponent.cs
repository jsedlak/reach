using Microsoft.AspNetCore.Components;

namespace Reach.Components;

public abstract class BaseTenantComponent : ComponentBase
{
    [Parameter]
    public string TenantSlug { get; set; } = null!;
}
