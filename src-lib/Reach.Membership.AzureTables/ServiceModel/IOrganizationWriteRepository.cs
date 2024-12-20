using Reach.Membership.Model;

namespace Reach.Membership.ServiceModel;

public interface IOrganizationWriteRepository
{
    Task CreateOrganization(Organization organization);

    Task AddHubToOrganization(Hub hub);
}
