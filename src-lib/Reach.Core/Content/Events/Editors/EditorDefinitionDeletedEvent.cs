namespace Reach.Content.Events.Editors;

[GenerateSerializer]
public class EditorDefinitionDeletedEvent : BaseEditorDefinitionEvent
{
    public EditorDefinitionDeletedEvent(Guid aggregateId) 
        : base(aggregateId)
    {
    }
}
