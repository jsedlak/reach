using Reach.Cqrs;
using Reach.Membership.Views;

namespace Reach.Silo.Membership.ServiceModel;

/// <summary>
/// Provides organization and hub level services
/// </summary>
public interface IOrganizationService
{
    /// <summary>
    /// Creates an Organization on behalf of a user
    /// </summary>
    /// <param name="id">The unique identifier for the organization</param>
    /// <param name="name">The friendly name of the organization</param>
    /// <param name="slug">The URL slug of the organization</param>
    /// <param name="ownerId">The owner's unique identifier</param>
    Task<CommandResponse> CreateOrganization(Guid id, string name, string slug, string ownerId);

    /// <summary>
    /// Creates a hub within an organization
    /// </summary>
    /// <param name="id">The unique identifier for the hub</param>
    /// <param name="organizationId">The unique identifier for the organization to which the hub will belong</param>
    /// <param name="name">The friendly name of the hub</param>
    /// <param name="slug">The URL slug of the hub</param>
    /// <param name="iconUrl">The URL representing the icon</param>
    Task<CommandResponse> CreateHub(Guid id, Guid organizationId, string name, string slug, string iconUrl);

    /// <summary>
    /// Gets all available organizations and hubs for the current user
    /// </summary>
    Task<IEnumerable<AvailableOrganizationView>> GetOrganizationsForUserAsync(string userId);

    /// <summary>
    /// Validates an organization's name and slug against the existing set
    /// </summary>
    /// <param name="organizationName"></param>
    /// <param name="organizationSlug"></param>
    /// <returns></returns>
    Task<bool> ValidateOrganization(string organizationName, string organizationSlug);
}
