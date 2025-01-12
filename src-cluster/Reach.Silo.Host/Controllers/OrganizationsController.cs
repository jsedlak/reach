using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reach.Cqrs;
using Reach.Membership.ApiModel;
using Reach.Security;
using Reach.Silo.Membership.ServiceModel;

namespace Reach.Silo.Host.Controllers;

[Authorize]
[Route("/api/organizations")]
public class OrganizationsController : Controller
{
    private readonly IOrganizationService _organizationService;

    public OrganizationsController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpPost]
    public async Task<CommandResponse> CreateOrganization([FromBody]CreateOrganizationRequest request)
    {
        await _organizationService.CreateOrganization(
            Guid.NewGuid(),
            request.OrganizationName,
            request.OrganizationSlug,
            User.GetIdentifierClaim()
        );

        var orgs = await _organizationService
            .GetOrganizationsForUserAsync(User.GetIdentifierClaim());

        var org = orgs.First(m => m.Slug == request.OrganizationSlug);

        await _organizationService.CreateHub(
            Guid.NewGuid(),
            org.Id,
            request.HubName,
            request.HubSlug,
            ""
        );

        return CommandResponse.Success();
    }

    [HttpPost("{organizationId}/hubs")]
    public async Task<CommandResponse> CreateHub([FromBody]CreateHubRequest request)
    {
        await _organizationService.CreateHub(
            Guid.NewGuid(),
            request.OrganizationId,
            request.HubName,
            request.HubSlug,
            ""
        );

        return CommandResponse.Success();
    }
}
