using Reach.Cqrs;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Reach.Content.Commands.FieldDefinitions;

[GenerateSerializer]
public class CreateFieldDefinitionCommand : AggregateCommand
{
    public CreateFieldDefinitionCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public CreateFieldDefinitionCommand(ResourceId resourceId)
        : base(resourceId.OrganizationId, resourceId.HubId, resourceId.AggregateId)
    {

    }

    public CreateFieldDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    [JsonPropertyName("name")]
    [Description("The name of the field definition")]
    public string Name { get; set; } = null!;

    [Id(1)]
    [JsonPropertyName("slug")]
    [Description("The technical, slug version of the field definition. Always a lowercase version of the name with every space replaced by a dash.")]
    public string Slug { get; set; } = null!;

    [Id(2)] 
    [Description("The unique identifier representing the editor definition this field definition references")] 
    public Guid EditorDefinitionId { get; set; }
}