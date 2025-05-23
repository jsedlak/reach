﻿using Microsoft.AspNetCore.Components;
using Reach.Components;
using Tazor.Components.Content;
using Tazor.Components.Navigation;

namespace Reach.Platform.Components.App;

public partial class Sidebar : ComponentBase
{
    private IEnumerable<object> _items = [];

    private readonly NavigationManager _navigation;
    private bool _isExpanded = true;

    public Sidebar(NavigationManager navigationManager)
    {
        _navigation = navigationManager;
        _navigation.LocationChanged += (_, __) => { StateHasChanged(); };

        ResetPaths();
    }

    protected override void OnInitialized()
    {
        ResetPaths();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        ResetPaths();
    }

    private void ResetPaths()
    {
        var basePath = string.Join("/", _navigation.ToBaseRelativePath(_navigation.Uri).Split(["/"], StringSplitOptions.RemoveEmptyEntries).Take(3));

        _items = [
            new NavItem { Text = "Dashboard", Href = $"{basePath}/", LeftIcon = HeroIcons.ChartBar(widthAndHeight: "size-4")},
            new NavGroup {
                Title = "Content",
                Icon = HeroIcons.DocumentMagnifyingGlass(widthAndHeight: "size-4"),
                Items = [
                    new NavItem { Text = "Components", Href = $"{basePath}/content/components", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                    new NavItem { Text = "Component Definitions", Href = $"{basePath}/content/component-definitions", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                    new NavItem { Text = "Field Definitions", Href = $"{basePath}/content/field-definitions", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                    new NavItem { Text = "Editor Definitions", Href = $"{basePath}/content/editor-definitions", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                    new NavItem { Text = "Renderer Definitions", Href = $"{basePath}/content/renderer-definitions", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                ]
            },
            new NavGroup {
                Title = "Pipelines",
                Icon = HeroIcons.InboxArrowDown(widthAndHeight: "size-4"),
                Items = [
                    new NavItem { Text = "Pipelines",  Href = $"{basePath}/pipelines", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                    new NavItem { Text = "Data Sources", Href = $"{basePath}/pipelines/data-sources", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                ]
            },
            new NavGroup {
                Title = "Endpoints",
                Icon = HeroIcons.PaperAirplane(widthAndHeight: "size-4"),
                Items = [
                    new NavItem { Text = "Endpoints", Href = $"{basePath}/data-sources", LeftIcon = HeroIcons.DocumentText(widthAndHeight: "size-4") },
                ]
            },
            new NavItem { Text = "Settings", Href = $"{basePath}/settings", LeftIcon = HeroIcons.Cog6Tooth(widthAndHeight: "size-4") },
        ];
    }


    private bool IsPathMatched(string path) =>
        new Uri(_navigation.Uri).LocalPath.Equals(path, StringComparison.OrdinalIgnoreCase);

    [CascadingParameter(Name = "CurrentOrganizationId")]
    public Guid? CurrentOrganizationId { get; set; }

    [CascadingParameter(Name = "CurrentHubId")]
    public Guid? CurrentHubId { get; set; }
}
