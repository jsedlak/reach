using Reach.EditorApp.ServiceModel;
using Reach.Orchestration.Model;
using Reach.Orchestration.ServiceModel;

namespace Reach.EditorApp.Services;

public sealed class ProviderRegionService : IRegionService
{
    private readonly IRegionProvider _regionProvider;

    public ProviderRegionService(IRegionProvider regionProvider)
    {
        _regionProvider = regionProvider;
    }

    public async Task<IEnumerable<Region>> GetAllRegionsAsync()
    {
        return await _regionProvider.GetAll();
    }
}