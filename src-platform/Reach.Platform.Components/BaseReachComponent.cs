using Microsoft.AspNetCore.Components;
using Reach.Membership.Views;

namespace Reach.Platform.Components;

public abstract class BaseReachComponent : ComponentBase
{
    protected override async Task OnParametersSetAsync()
    {
        if(CurrentOrganizationId != null && CurrentHubId != null)
        {
            await OnComponentIsReady();
        }
    }

    protected virtual Task OnComponentIsReady()
    {
        return Task.CompletedTask;
    } 

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

    [CascadingParameter(Name = "Organizations")]
    protected IEnumerable<AvailableOrganizationView>? Organizations { get; set; }

    protected AvailableOrganizationView? CurrentOrganization
    {
        get
        {
            return Organizations?.FirstOrDefault(m => m.Id == CurrentOrganizationId);
        }
    }

    protected AvailableHubView? CurrentHub
    {
        get
        {
            return CurrentOrganization?.Hubs.FirstOrDefault(m => m.Id == CurrentHubId);
        }
    }

    /// <summary>
    /// Gets whether or not the component is ready to render by validating the organization and hub context
    /// </summary>
    protected bool IsReady => CurrentOrganizationId != null && CurrentHubId != null;
}