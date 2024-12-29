namespace Reach.Content.Events.RendererDefinitions;

[GenerateSerializer]
public class RendererDefinitionRenamedEvent : BaseRendererDefinitionEvent
{
    public RendererDefinitionRenamedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }


    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Slug { get; set; } = null!;
}