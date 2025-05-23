﻿using Microsoft.AspNetCore.Components;

namespace Reach.Apps.ContentApp.Components.Layout;

public partial class ContentSublayout : ComponentBase
{
    [Parameter]
    public string Title { get; set; } = "Untitled Page";

    [Parameter]
    public string Subtitle { get; set; } = "";

    [Parameter]
    public RenderFragment? Toolbar { get; set; } = null;

    [Parameter]
    public required RenderFragment Body { get; set; } = null!;

    [Inject]
    protected NavigationManager Navigation { get; set; } = null!;

    [CascadingParameter(Name = "IsReady")] public bool IsReady { get; set; }
}
