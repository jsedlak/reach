using Reach.Cqrs;

namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public class FieldDefinitionEditorSetEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionEditorSetEvent(Guid aggregateId, Guid tenantId)
        : base(aggregateId, tenantId)
    {
    }

    [Id(0)]
    public Guid EditorDefinitionId { get; set; }
}