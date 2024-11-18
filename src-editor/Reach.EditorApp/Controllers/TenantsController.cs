using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reach.Membership.Views;

namespace Reach.EditorApp.Controllers;

public class TenantsController : Controller
{
    [Authorize]
    public IEnumerable<AvailableTenantView> GetTenants()
    {
        return [];
    }
}
