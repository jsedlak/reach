namespace Reach.Membership.Views;

/// <summary>
/// Represents a tenant that the current user has access to in some manner
/// </summary>
public class AvailableTenantView
{
    public Guid TenantId { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string IconUrl { get; set; } = string.Empty;
}
