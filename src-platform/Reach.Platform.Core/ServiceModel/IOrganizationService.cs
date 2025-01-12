using Reach.Cqrs;
using Reach.Membership.ApiModel;
using Reach.Membership.Views;

namespace Reach.Platform.ServiceModel;

public interface IOrganizationService
{
    Task<IEnumerable<AvailableOrganizationView>> GetAvailableOrganizations();

    Task<CommandResponse> Onboard(CreateOrganizationRequest request);
}