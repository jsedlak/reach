using Microsoft.AspNetCore.Components;
using Reach.Membership.Views;

namespace Reach.Platform.Pages;

public partial class Home : ComponentBase
{
    [CascadingParameter(Name = "Organizations")]
    public IEnumerable<AvailableOrganizationView>? Organizations { get; set; }
}
