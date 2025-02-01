using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reach.Cqrs;
using Reach.Membership.ApiModel;
using Reach.Security;
using Reach.Silo.Membership.GrainModel;
using Reach.Silo.Membership.ServiceModel;

namespace Reach.Silo.Host.Controllers;

[Authorize]
[Route("/api/organizations")]
public class OrganizationsController : Controller
{
    private readonly IOrganizationService _organizationService;
    private readonly IClusterClient _clusterClient;

    public OrganizationsController(
        IOrganizationService organizationService,
        IClusterClient clusterClient)
    {
        _organizationService = organizationService;
        _clusterClient = clusterClient;
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
        var hubId = Guid.NewGuid();

        await _organizationService.CreateHub(
            hubId,
            org.Id,
            request.HubName,
            request.HubSlug,
            ""
        );

        var mgmtGrain = _clusterClient.GetGrain<IHubManagementGrain>(
            $"{org.Id}/{hubId}"
        );

        await mgmtGrain.Initialize();

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
