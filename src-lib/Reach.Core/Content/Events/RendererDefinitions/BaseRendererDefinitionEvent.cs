using Reach.Cqrs;

namespace Reach.Content.Events.RendererDefinitions;

[GenerateSerializer]
public class BaseRendererDefinitionEvent : BaseEvent
{
    public BaseRendererDefinitionEvent(Guid aggregateId, Guid organizationId, Guid hubId) 
        : base(aggregateId, organizationId, hubId)
    {
    }
}
