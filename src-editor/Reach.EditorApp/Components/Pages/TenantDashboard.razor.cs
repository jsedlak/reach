using Microsoft.AspNetCore.Components;

namespace Reach.EditorApp.Components.Pages;

public partial class TenantDashboard : ComponentBase
{
    [Parameter]
    public string TenantSlug { get; set; } = string.Empty;
}
