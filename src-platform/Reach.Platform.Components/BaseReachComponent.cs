using Microsoft.AspNetCore.Components;

namespace Reach.Platform.Components;

public class BaseReachComponent : ComponentBase
{
    /// <summary>
    /// Gets the cascading organization id
    /// </summary>
    [CascadingParameter(Name = "CurrentOrganizationId")]
    protected Guid? CurrentOrganizationId { get; set; }

    /// <summary>
    /// Gets the cascading hub id
    /// </summary>
    [CascadingParameter(Name = "CurrentHubId")]
    protected Guid? CurrentHubId { get; set; }

    /// <summary>
    /// Gets whether or not the component is ready to render by validating the organization and hub context
    /// </summary>
    protected bool IsReady => CurrentOrganizationId != null && CurrentHubId != null;
}