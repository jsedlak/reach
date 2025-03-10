namespace Reach.Pipelines.Events;

[GenerateSerializer]
public class NodeRemovedFromPipelineEvent : BasePipelineEvent
{
    public NodeRemovedFromPipelineEvent(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid NodeId { get; set; }
}
