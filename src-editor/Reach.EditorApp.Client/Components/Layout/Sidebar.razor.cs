using Microsoft.AspNetCore.Components;
using Tazor.Components.Content;
using Tazor.Components.Navigation;

namespace Reach.EditorApp.Client.Components.Layout;

public partial class Sidebar : ComponentBase
{
    private readonly IEnumerable<object> _items = [];
    private readonly NavigationManager _navigation;
    private bool _isExpanded = true;

    public Sidebar(NavigationManager navigationManager)
    {
        _navigation = navigationManager;
        _navigation.LocationChanged += (_, __) => { StateHasChanged(); };

        _items = [
            new NavItem { Text = "Dashboard", Href = "/", LeftIcon = HeroIcons.ChartBar(widthAndHeight: "size-4")},
            new NavGroup {
                Title = "Content",
                Icon = HeroIcons.DocumentMagnifyingGlass(widthAndHeight: "size-4"),
                Items = [
                    new NavItem { Text = "Content", Href = "/apps/content", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                    new NavItem { Text = "Components", Href = "/apps/content/components", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                    new NavItem { Text = "Fields", Href = "/apps/content/fields", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                    new NavItem { Text = "Field Definitions", Href = "/apps/content/field-definitions", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                    new NavItem { Text = "Editor Definitions", Href = "/apps/content/editor-definitions", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                    new NavItem { Text = "Renderer Definitions", Href = "/apps/content/renderer-definitions", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                ]
            },
            new NavGroup {
                Title = "Pipelines",
                Icon = HeroIcons.InboxArrowDown(widthAndHeight: "size-4"),
                Items = [
                    new NavItem { Text = "Workflows",  Href = "/apps/pipelines/workflows", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                    new NavItem { Text = "Data Sources", Href = "/apps/pipelines/data-sources", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                ]
            },
            new NavGroup {
                Title = "Endpoints",
                Icon = HeroIcons.PaperAirplane(widthAndHeight: "size-4"),
                Items = [
                    new NavItem { Text = "Endpoints", Href = "/apps/data-sources", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                ]
            },
            new NavItem { Text = "Settings", Href = "/settings", LeftIcon = HeroIcons.Cog6Tooth(widthAndHeight: "size-4") },
        ];
    }

    private bool IsPathMatched(string path) =>
        new Uri(_navigation.Uri).LocalPath.Equals(path, StringComparison.OrdinalIgnoreCase);
}
