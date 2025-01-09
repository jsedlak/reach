using Reach.Membership.ServiceModel;
using Reach.Membership.Views;
using System.Security.Claims;
using Reach.Security;

namespace Reach.Silo.Host.Queries;

[ExtendObjectType("Query")]
public class OrganizationQueries
{
    public Task<IEnumerable<AvailableOrganizationView>> Organizations(
        ClaimsPrincipal claimsPrincipal,
        [Service] IOrganizationService organizationService) =>
        organizationService.GetOrganizationsForUserAsync(claimsPrincipal.GetIdentifierClaim());
}