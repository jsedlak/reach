namespace Reach.Content.Events.RendererDefinitions;

[GenerateSerializer]
public class RendererDefinitionDeletedEvent : BaseRendererDefinitionEvent
{
    public RendererDefinitionDeletedEvent(Guid aggregateId, Guid organizationId, Guid hubId) 
        : base(aggregateId, organizationId, hubId)
    {
    }
}
