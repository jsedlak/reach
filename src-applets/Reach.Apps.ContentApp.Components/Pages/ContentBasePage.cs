using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Reach.Components;
using Reach.Membership.Views;
using Tazor.Components.Theming;

namespace Reach.Apps.ContentApp.Components.Pages;

public abstract class ContentBasePage : BaseTenantComponent
{
    [CascadingParameter(Name = "CurrentOrganizationId")]
    public Guid? CurrentOrganizationId { get; set; }
    
    [CascadingParameter(Name = "CurrentHubId")]
    public Guid? CurrentHubId { get; set; }
    
    [CascadingParameter(Name = "Organizations")]
    public IEnumerable<AvailableOrganizationView>? Organizations { get; set; }

    public AvailableOrganizationView? CurrentOrganization
    {
        get
        {
            return Organizations?.FirstOrDefault(m => m.Id == CurrentOrganizationId);
        }
    }

    public AvailableHubView? CurrentHub
    {
        get
        {
            return CurrentOrganization?.Hubs?.FirstOrDefault(m => m.Id == CurrentHubId);
        }
    }
}
