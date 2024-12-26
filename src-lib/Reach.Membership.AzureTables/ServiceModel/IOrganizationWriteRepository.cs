using Reach.Membership.Model;

namespace Reach.Membership.ServiceModel;

internal interface IOrganizationWriteRepository
{
    Task CreateOrganization(Organization organization);

    Task AddHubToOrganization(Hub hub);
}
