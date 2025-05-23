﻿using Microsoft.AspNetCore.Components;
using Tazor.Components.Navigation;

namespace Reach.Platform.Components.App;

public class NavGroup
{
    public required string Title { get; init; }

    public required MarkupString Icon { get; init; }

    public required NavItem[] Items { get; set; }

    public bool IsExpanded { get; set; }
}