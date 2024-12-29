namespace Reach.Content.Events.Editors;

[GenerateSerializer]
public class EditorDefinitionDeletedEvent : BaseEditorDefinitionEvent
{
    public EditorDefinitionDeletedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}
