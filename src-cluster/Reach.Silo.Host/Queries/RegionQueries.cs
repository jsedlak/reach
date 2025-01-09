using Reach.Orchestration.Model;
using Reach.Orchestration.ServiceModel;

namespace Reach.Silo.Host.Queries;

[ExtendObjectType("Query")]
public class RegionQueries
{
    public Task<IEnumerable<Region>> Regions([Service] IRegionProvider regionProvider) =>
        regionProvider.GetAll();
}
