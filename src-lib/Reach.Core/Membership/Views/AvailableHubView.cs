namespace Reach.Membership.Views;

public class AvailableHubView
{
    public Guid Id { get; set; }

    public Guid OrganizationId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string IconUrl { get; set; } = string.Empty;
}