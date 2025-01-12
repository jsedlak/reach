namespace Reach.Membership.ApiModel;

public sealed class CreateHubRequest
{
    public string HubName { get; set; } = null!;

    public string HubSlug { get; set; } = null!;

    public Guid OrganizationId { get; set; }
}
