using Reach.Content.Model;

namespace Reach.Content.Events.FieldDefinitions;

[GenerateSerializer]
public class FieldDefinitionCreatedEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionCreatedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)] public string Name { get; set; } = null!;

    [Id(1)] public string Key { get; set; } = null!;

    [Id(2)] public string? Group { get; set; }

    [Id(3)] public Guid EditorDefinitionId { get; set; }

    [Id(4)] public EditorParameter[] EditorParameters { get; set; } = Array.Empty<EditorParameter>();
}