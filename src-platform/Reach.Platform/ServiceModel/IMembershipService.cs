using Reach.Membership.Views;

namespace Reach.Platform.ServiceModel;

public interface IMembershipService
{
    Task<IEnumerable<AvailableOrganizationView>> GetAvailableOrganizations();
}
