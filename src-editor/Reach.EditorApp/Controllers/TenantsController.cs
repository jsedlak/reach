using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reach.Cqrs;
using Reach.EditorApp.ApiModel;
using Reach.Membership.ServiceModel;
using Reach.Membership.Views;
using Reach.Orchestration.ServiceModel;

namespace Reach.EditorApp.Controllers;

[ApiController]
[Route("api/organizations")]
public class OrganizationsController : Controller
{
    private readonly IOrganizationService _organizationService;
    private readonly IRegionProvider _regionProvider;

    public OrganizationsController(IOrganizationService organizationService, IRegionProvider regionProvider)
    {
        _organizationService = organizationService;
        _regionProvider = regionProvider;
    }

    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<AvailableOrganizationView>> GetOrganizations()
    {
        var userId = HttpContext.User.Identity?.Name ?? "uhoh";
        var result = await _organizationService.GetOrganizationsForUserAsync(userId);
        return result;
    }

    [Authorize]
    [HttpPost]
    public async Task<CommandResponse> Create([FromBody] CreateOrganizationRequest request)
    {
        var userId = HttpContext.User.Identity?.Name ?? "uhoh";

        var result = await _organizationService.CreateOrganization(request.Id, request.Name, request.Slug, userId);
        return result;
    }

    [Authorize]
    [HttpPost("{organizationId}/hubs")]
    public async Task<CommandResponse> Create([FromRoute] Guid organizationId, [FromBody] CreateHubRequest request)
    {
        var userId = HttpContext.User.Identity?.Name ?? "uhoh";

        var result = await _organizationService.CreateHub(request.Id, organizationId, request.Name, request.Slug, request.IconUrl, request.RegionKey);
        return result;
    }

}
