namespace Reach.Pipelines.Events;

[GenerateSerializer]
public class TransformerRemovedFromVertexEvent : BasePipelineEvent
{
    public TransformerRemovedFromVertexEvent(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid NodeId { get; set; }

    [Id(1)]
    public Guid VertexId { get; set; }

    [Id(2)]
    public Guid TransformerId { get; set; }
}
