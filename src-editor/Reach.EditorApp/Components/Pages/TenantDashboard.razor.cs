using Microsoft.AspNetCore.Components;
using Reach.Components;
using Reach.Components.Context;

namespace Reach.EditorApp.Components.Pages;

public partial class TenantDashboard : BaseTenantComponent
{
    [CascadingParameter]
    public TenantContext TenantContext { get; set; } = null!;
}
