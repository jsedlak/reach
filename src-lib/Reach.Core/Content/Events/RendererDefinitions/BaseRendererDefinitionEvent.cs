using Reach.Cqrs;

namespace Reach.Content.Events.RendererDefinitions;

[GenerateSerializer]
public class BaseRendererDefinitionEvent : BaseEvent
{
    public BaseRendererDefinitionEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}
