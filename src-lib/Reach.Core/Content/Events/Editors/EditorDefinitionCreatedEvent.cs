namespace Reach.Content.Events.Editors;

[GenerateSerializer]
public class EditorDefinitionCreatedEvent : BaseEditorDefinitionEvent
{
    public EditorDefinitionCreatedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(2)]
    public string EditorType { get; set; } = null!;
}
