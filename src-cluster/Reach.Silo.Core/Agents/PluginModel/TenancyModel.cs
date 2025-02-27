using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Reach.Silo.Agents.PluginModel;

public class TenancyModel
{
    [JsonPropertyName("organizationId")]
    [Description("The organizationId represents the unique identifier of the organization this request is for")]
    public Guid OrganizationId { get; set; }

    [JsonPropertyName("hubId")]
    [Description("The hubId represents the unique identifier of the hub this request belongs to")]
    public Guid HubId { get; set; }
}
