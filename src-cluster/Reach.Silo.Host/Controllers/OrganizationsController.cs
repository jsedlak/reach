using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reach.Cqrs;
using Reach.Orchestration.ApiModel;

namespace Reach.Silo.Host.Controllers;

[Authorize]
[Route("/api/organizations")]
public class OrganizationsController : Controller
{
    [HttpPost]
    public Task<CommandResponse> CreateOrganization([FromBody]CreateOrganizationRequest request)
    {
        return Task.FromResult(
            CommandResponse.Success()
        );
    }

    [HttpPost("{organizationId}/hubs")]
    public Task<CommandResponse> CreateHub([FromBody]CreateHubRequest request)
    {
        return Task.FromResult(
            CommandResponse.Success()
        );
    }
}