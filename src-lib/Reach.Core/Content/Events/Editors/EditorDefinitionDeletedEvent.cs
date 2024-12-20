namespace Reach.Content.Events.Editors;

[GenerateSerializer]
public class EditorDefinitionDeletedEvent : BaseEditorDefinitionEvent
{
    public EditorDefinitionDeletedEvent(Guid aggregateId, Guid organizationId, Guid hubId)
        : base(aggregateId, organizationId, hubId)
    {
    }
}
