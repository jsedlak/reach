using Reach.Cqrs;

namespace Reach.Pipelines.Events;

[GenerateSerializer]
public class BasePipelineEvent : BaseEvent
{
    public BasePipelineEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}
