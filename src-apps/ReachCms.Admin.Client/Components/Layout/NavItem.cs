using Microsoft.AspNetCore.Components;

namespace ReachCms.Admin.Client.Components.Layout;

public class NavItem
{
    public string Title { get; set; } = null!;

    public string Href { get; set; } = null!;

    public MarkupString? Icon { get; set; } = null;
}
