namespace Reach.Content.Events.FieldDefinitions;

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