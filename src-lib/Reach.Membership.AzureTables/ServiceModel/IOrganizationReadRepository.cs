using Reach.Membership.Model;

namespace Reach.Membership.ServiceModel;

internal interface IOrganizationReadRepository
{
    Task<IEnumerable<Organization>> GetOrganizationsForUser(string userId);

    Task<IEnumerable<Hub>> GetHubsForOrganization(Guid organizationId);

    Task<IEnumerable<Hub>> GetHubsForOrganizations(IEnumerable<Guid> organizationIds);
}
