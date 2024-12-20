namespace Reach.EditorApp.ApiModel;

public class CreateHubRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string RegionKey { get; set; } = string.Empty;

    public string IconUrl { get; set; } = string.Empty;

    public Guid OrganizationId { get; set; }
}