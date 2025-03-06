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

    [HttpPost("validate")]
    public async Task<CommandResponse> ValidateOrgName([FromBody] ValidateOrgNameRequest request)
    {
        var result = await _organizationService.ValidateOrganization(request.Name, request.Slug);

        return new() { IsSuccess = result };
    }

    [HttpPost]
    public async Task<CommandResponse> CreateOrganization([FromBody] CreateOrganizationRequest request)
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
            request.IconUrl
        );

        var mgmtGrain = _clusterClient.GetGrain<IHubManagementGrain>(
            $"{org.Id}/{hubId}"
        );

        await mgmtGrain.Initialize();

        return CommandResponse.Success();
    }

    [HttpPost("{organizationId}/hubs/validate")]
    public async Task<CommandResponse> ValidateHubName([FromBody] ValidateHubNameRequest request)
    {
        var existingOrgs = await _organizationService.GetOrganizationsForUserAsync(
            User.GetIdentifierClaim()
        );

        var existing = existingOrgs.Any(m => m.Hubs.Any(h => h.Slug.Equals(request.Slug)));
        return new() { IsSuccess = !existing };
    }

    [HttpPost("{organizationId}/hubs")]
    public async Task<CommandResponse> CreateHub([FromBody] CreateHubRequest request)
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
