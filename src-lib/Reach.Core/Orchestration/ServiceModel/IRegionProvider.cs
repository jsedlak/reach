using Reach.Orchestration.Model;

namespace Reach.Orchestration.ServiceModel;

public interface IRegionProvider
{
    Task<IEnumerable<Region>> GetAll();

    Task<Region> GetByKey(string key);
}