using Microsoft.AspNetCore.Mvc;
using Reach.Orchestration.Model;
using Reach.Orchestration.ServiceModel;

namespace Reach.EditorApp.Controllers;

[ApiController]
[Route("api/regions")]
public class RegionsController : Controller
{
    private readonly IRegionProvider _regionProvider;

    public RegionsController(IRegionProvider regionProvider)
    {
        _regionProvider = regionProvider;
    }

    public async Task<IEnumerable<Region>> GetRegions()
    {
        return await _regionProvider.GetAll();
    }
}