namespace Reach.Content.Events.EditorDefinitions;

[GenerateSerializer]
public class EditorDefinitionCreatedEvent : BaseEditorDefinitionEvent
{
    public EditorDefinitionCreatedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Slug { get; set; } = null!;

    [Id(2)]
    public string EditorType { get; set; } = null!;
}
