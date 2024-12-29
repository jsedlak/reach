using Reach.Cqrs;

namespace Reach.Content.Events.Components;

[GenerateSerializer]
public class BaseComponentEvent : BaseEvent
{
    public BaseComponentEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}