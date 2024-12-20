using Reach.Cqrs;
using Reach.Membership.Views;

namespace Reach.Membership.ServiceModel;

/// <summary>
/// Provides organization and hub level services
/// </summary>
public interface IOrganizationService
{
    Task<CommandResponse> CreateOrganization(Guid id, string name, string slug, string ownerId);

    Task<CommandResponse> CreateHub(Guid id, Guid organizationId, string name, string slug, string iconUrl, string regionKey);

    /// <summary>
    /// Gets all available organizations and hubs for the current user
    /// </summary>
    /// <returns></returns>
    // TODO: Remove this and use the userId passed option
    [Obsolete]
    Task<IEnumerable<AvailableOrganizationView>> GetOrganizationsForUserAsync();

    /// <summary>
    /// Gets all available organizations and hubs for the current user
    /// </summary>
    Task<IEnumerable<AvailableOrganizationView>> GetOrganizationsForUserAsync(string userId);
}