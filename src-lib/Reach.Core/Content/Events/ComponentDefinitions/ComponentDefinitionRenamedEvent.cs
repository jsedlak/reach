namespace Reach.Content.Events.ComponentDefinitions;

[GenerateSerializer]
public class ComponentDefinitionRenamedEvent : BaseComponentDefinitionEvent
{
    public ComponentDefinitionRenamedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Slug { get; set; } = null!;
}