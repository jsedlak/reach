using Reach.Cqrs;

namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public class FieldDefinitionEditorSetEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionEditorSetEvent(Guid aggregateRootId)
        : base(aggregateRootId)
    {
    }

    [Id(0)]
    public Guid EditorDefinitionId { get; set; }
}