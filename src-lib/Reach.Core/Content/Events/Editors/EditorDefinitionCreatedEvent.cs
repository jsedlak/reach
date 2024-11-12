namespace Reach.Content.Events.Editors;

[GenerateSerializer]
public class EditorDefinitionCreatedEvent : BaseEditorDefinitionEvent
{
    public EditorDefinitionCreatedEvent(Guid aggregateId) 
        : base(aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(2)]
    public string EditorType { get; set; } = null!;
}
