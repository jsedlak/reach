namespace Reach.Orchestration.ApiModel;

public sealed class CreateHubRequest
{
    public string HubName { get; set; } = null!;

    public string HubSlug { get; set; } = null!;

}
