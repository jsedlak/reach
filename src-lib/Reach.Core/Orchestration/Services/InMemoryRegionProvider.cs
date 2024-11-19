using Reach.Orchestration.Model;
using Reach.Orchestration.ServiceModel;

namespace Reach.Orchestration.Services;

public sealed class InMemoryRegionProvider : IRegionProvider
{
    private readonly IEnumerable<Region> _regions;

    public InMemoryRegionProvider(IRegionUrlFormatter formatter, IEnumerable<Region> regions)
    {
        _regions = regions;

        foreach (var region in regions)
        {
            region.ApiUrl = formatter.GetApiBaseUrl(region);
            region.GraphUrl = formatter.GetGraphBaseUrl(region);
        }
    }

    public Task<IEnumerable<Region>> GetAll()
    {
        return Task.FromResult(_regions);
    }

    public Task<Region> GetByKey(string key)
    {
        return Task.FromResult(
            _regions.First(m => m.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
        );
    }
}