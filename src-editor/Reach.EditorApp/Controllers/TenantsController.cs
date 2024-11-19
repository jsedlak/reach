using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reach.Cqrs;
using Reach.Membership.Model;
using Reach.Membership.ServiceModel;
using Reach.Membership.Views;
using Reach.Orchestration.ServiceModel;

namespace Reach.EditorApp.Controllers;

[ApiController]
[Route("api/tenants")]
public class TenantsController : Controller
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IRegionProvider _regionProvider;

    public TenantsController(ITenantRepository tenantRepository, IRegionProvider regionProvider)
    {
        _tenantRepository = tenantRepository;
        _regionProvider = regionProvider;
    }

    [Authorize]
    [HttpGet]
    public async Task<IEnumerable<AvailableTenantView>> GetTenants()
    {
        // TODO: Move this into a service class
        var userId = HttpContext.User.Identity?.Name ?? "uhoh";
        var result = await _tenantRepository.GetAllForUser(userId);

        var regions = await _regionProvider.GetAll();

        return result.Select(m => new AvailableTenantView
        {
            TenantId = m.Id,
            Name = m.Name,
            Slug = m.Slug,
            Region = regions.First(region => region.Key == m.RegionId),
            IconUrl = $"https://picsum.photos/seed/{m.Id}/200"
        });
    }

    [Authorize]
    [HttpPost]
    public async Task<CommandResponse> Create([FromBody] Tenant tenant)
    {
        var userId = HttpContext.User.Identity?.Name ?? "uhoh";
        tenant.OwnerIdentifier = userId;

        var result = await _tenantRepository.Create(tenant);
        return result;
    }


}
