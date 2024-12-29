using Reach.Cqrs;

namespace Reach.Content.Events.Content;

[GenerateSerializer]
public class BaseContentEvent : BaseEvent
{
    public BaseContentEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}