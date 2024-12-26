namespace Reach.Content.Events.Editors;

[GenerateSerializer]
public class EditorDefinitionCreatedEvent : BaseEditorDefinitionEvent
{
    public EditorDefinitionCreatedEvent(Guid aggregateId, Guid organizationId, Guid hubId)
        : base(aggregateId, organizationId, hubId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(2)]
    public string EditorType { get; set; } = null!;
}
