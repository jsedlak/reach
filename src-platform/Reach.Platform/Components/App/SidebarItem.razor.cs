﻿using Microsoft.AspNetCore.Components;
using Tazor.Components.Navigation;

namespace Reach.Platform.Components.App;

public partial class SidebarItem : ComponentBase
{
    [Parameter]
    public NavItem Item { get; set; } = null!;

    [Parameter]
    public bool IsCurrent { get; set; }

    [Parameter]
    public bool IsExpanded { get; set; }
}
