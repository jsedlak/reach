using Microsoft.AspNetCore.Components;
using Tazor.Components.Theming;

namespace Reach.Components;

public abstract class BaseTenantComponent : ComponentBase
{
    [Parameter]
    public string OrganizationSlug { get; set; } = null!;

    [Parameter]
    public string HubSlug { get; set; } = null!;

    /// <summary>
    /// Gets or Sets the theme that has been cascaded to this component
    /// </summary>
    [CascadingParameter(Name = "Theme")]
    public ITheme Theme { get; set; } = null!;
}
