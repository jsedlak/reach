using Reach.Membership.Views;
using System.Security.Claims;
using Reach.Security;
using Reach.Silo.Membership.ServiceModel;

namespace Reach.Silo.Host.Queries;

[ExtendObjectType("Query")]
public class OrganizationQueries
{
    public Task<IEnumerable<AvailableOrganizationView>> Organizations(
        ClaimsPrincipal claimsPrincipal,
        [Service] IOrganizationService organizationService) =>
        organizationService.GetOrganizationsForUserAsync(claimsPrincipal.GetIdentifierClaim());
}