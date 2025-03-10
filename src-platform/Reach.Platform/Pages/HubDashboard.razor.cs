using Microsoft.AspNetCore.Components;
using Reach.Platform.Components;

namespace Reach.Platform.Pages;

public partial class HubDashboard : BaseReachComponent
{
    [Parameter] public string OrgSlug { get; set; } = null!;

    [Parameter] public string HubSlug { get; set; } = null!;
}