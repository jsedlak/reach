namespace Reach.Membership.ApiModel;

public sealed class CreateOrganizationRequest
{
    public string OrganizationName { get; set; } = null!;

    public string OrganizationSlug { get; set; } = null!;

    public string HubName { get; set; } = null!;

    public string HubSlug { get; set; } = null!;
}
