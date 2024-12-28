namespace Reach.Content.Events.RendererDefinitions;

[GenerateSerializer]
public class RendererDefinitionCreatedEvent : BaseRendererDefinitionEvent
{
    public RendererDefinitionCreatedEvent(Guid aggregateId, Guid organizationId, Guid hubId) 
        : base(aggregateId, organizationId, hubId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Slug { get; set; } = null!;
}
