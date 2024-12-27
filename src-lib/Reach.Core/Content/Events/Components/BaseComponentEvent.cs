using Reach.Cqrs;

namespace Reach.Content.Events.Components;

[GenerateSerializer]
public class BaseComponentEvent : BaseEvent
{
    public BaseComponentEvent(Guid aggregateId, Guid organizationId, Guid hubId) 
        : base(aggregateId, organizationId, hubId)
    {
    }
}