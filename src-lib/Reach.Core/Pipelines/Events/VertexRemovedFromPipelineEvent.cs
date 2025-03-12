namespace Reach.Pipelines.Events;

[GenerateSerializer]
public class VertexRemovedFromPipelineEvent : BasePipelineEvent
{
    public VertexRemovedFromPipelineEvent(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid SourceNodeId { get; set; }

    [Id(1)]
    public Guid VertexId { get; set; }
}