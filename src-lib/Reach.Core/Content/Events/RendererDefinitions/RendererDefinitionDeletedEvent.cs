namespace Reach.Content.Events.RendererDefinitions;

[GenerateSerializer]
public class RendererDefinitionDeletedEvent : BaseRendererDefinitionEvent
{
    public RendererDefinitionDeletedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}
