using Reach.Orchestration.Model;

namespace Reach.EditorApp.ServiceModel;

/// <summary>
/// Defines common interaction points for the editor application to deal with regions
/// </summary>
public interface IRegionService
{
    /// <summary>
    /// Gets all regions supported by the host
    /// </summary>
    Task<IEnumerable<Region>> GetAllRegionsAsync();
}