using Reach.Cqrs;

namespace Reach.Content.Events.Content;

[GenerateSerializer]
public class BaseContentEvent : BaseEvent
{
    public BaseContentEvent(Guid aggregateId, Guid organizationId, Guid hubId)
        : base(aggregateId, organizationId, hubId)
    {
    }
}