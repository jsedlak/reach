using Microsoft.AspNetCore.Components;

namespace Reach.Platform.Pages;

public partial class HubDashboard : ComponentBase
{
    [Parameter] public string OrgSlug { get; set; } = null!;

    [Parameter] public string HubSlug { get; set; } = null!;
}