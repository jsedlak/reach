namespace Reach.Content.Events.Components;

[GenerateSerializer]
public class ComponentRenamedEvent : BaseComponentEvent
{
    public ComponentRenamedEvent(Guid aggregateId, Guid organizationId, Guid hubId) 
        : base(aggregateId, organizationId, hubId)
    {
    }

    [Id(0)] public string Name { get; set; } = null!;

    [Id(1)] public string Slug { get; set; } = null!;
}