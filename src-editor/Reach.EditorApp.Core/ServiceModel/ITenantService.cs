using Reach.Cqrs;
using Reach.Membership.Model;
using Reach.Membership.Views;

namespace Reach.EditorApp.ServiceModel;

/// <summary>
/// Defines common tenant interaction points for the editor application
/// </summary>
public interface ITenantService
{
    /// <summary>
    /// Gets all tenants for the current user
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<AvailableTenantView>> GetTenantsForUserAsync();

    /// <summary>
    /// Creates a tenant for the user
    /// </summary>
    /// <param name="tenant">The tenant to register</param>
    Task<CommandResponse> CreateAsync(Tenant tenant);
}
