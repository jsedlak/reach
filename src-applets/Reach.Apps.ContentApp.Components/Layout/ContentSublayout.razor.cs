using Microsoft.AspNetCore.Components;
using Tazor.Components.Content;
using Tazor.Components.Navigation;

namespace Reach.Apps.ContentApp.Components.Layout;

public partial class ContentSublayout : ComponentBase
{
    private IEnumerable<NavItem> _navigationItems = [
        new NavItem {
            Text = "Content",
            Href = "/apps/content",
            LeftIcon = HeroIcons.Document("inline-block mr-2")
        },
        new NavItem {
            Text = "Components",
            Href = "/apps/content/components",
            LeftIcon = HeroIcons.PuzzlePiece("inline-block mr-2")
        },
        new NavItem {
            Text = "Fields",
            Href = "/apps/content/fields",
            LeftIcon = HeroIcons.RectangleStack("inline-block mr-2")
        },
        new NavItem {
            Text = "Field Definitions",
            Href = "/apps/content/field-definitions",
            LeftIcon = HeroIcons.QueueList("inline-block mr-2")
        },
        new NavItem {
            Text = "Editor Definitions",
            Href = "/apps/content/editor-definitions",
            LeftIcon = new MarkupString(@"<svg viewBox=""0 0 256 256"" class=""inline-block mr-2 w-5 h-5"" xmlns=""http://www.w3.org/2000/svg""><rect fill=""none"" height=""256"" width=""256""></rect><line fill=""none"" stroke=""currentColor"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""12"" x1=""112"" x2=""112"" y1=""48"" y2=""208""></line><path d=""M144,72h88a8,8,0,0,1,8,8v96a8,8,0,0,1-8,8H144"" fill=""none"" stroke=""currentColor"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""12""></path><path d=""M112,184H24a8,8,0,0,1-8-8V80a8,8,0,0,1,8-8h88"" fill=""none"" stroke=""currentColor"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""12""></path><line fill=""none"" stroke=""currentColor"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""12"" x1=""50"" x2=""78"" y1=""112"" y2=""112""></line><line fill=""none"" stroke=""currentColor"" stroke-linecap=""round"" stroke-linejoin=""round"" stroke-width=""12"" x1=""64"" x2=""64"" y1=""112"" y2=""148""></line></svg>")
        }
    ];

    private bool IsPathMatched(string path) => 
        new Uri(Navigation.Uri).LocalPath.Equals(path, StringComparison.OrdinalIgnoreCase);

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
}
