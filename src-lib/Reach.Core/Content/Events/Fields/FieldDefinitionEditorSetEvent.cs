using Reach.Cqrs;

namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public class FieldDefinitionEditorSetEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionEditorSetEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid EditorDefinitionId { get; set; }
}