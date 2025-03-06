namespace Reach.Membership.ApiModel;

public class ValidateHubNameRequest
{
    public Guid OrganizationId { get; set; }

    public string Name { get; set; } = "";

    public string Slug { get; set; } = "";
}
