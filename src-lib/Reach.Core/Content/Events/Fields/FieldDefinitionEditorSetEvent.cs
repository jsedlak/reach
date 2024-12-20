using Reach.Cqrs;

namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public class FieldDefinitionEditorSetEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionEditorSetEvent(Guid aggregateId, Guid organizationId, Guid hubId)
        : base(aggregateId, organizationId, hubId)
    {
    }

    [Id(0)]
    public Guid EditorDefinitionId { get; set; }
}