using Reach.Content.Model;

namespace Reach.Content.Events.Components;

[GenerateSerializer]
public class ComponentCreatedEvent : BaseComponentEvent
{
    public ComponentCreatedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)] public string Name { get; set; } = null!;

    [Id(1)] public string Slug { get; set; } = null!;

    [Id(2)] public Guid DefinitionId { get; set; }

    [Id(3)] public Field[] Fields { get; set; } = Array.Empty<Field>();
}