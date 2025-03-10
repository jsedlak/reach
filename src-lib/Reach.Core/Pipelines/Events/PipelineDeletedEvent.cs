namespace Reach.Pipelines.Events;

[GenerateSerializer]
public class PipelineDeletedEvent : BasePipelineEvent
{
    public PipelineDeletedEvent(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }
}
