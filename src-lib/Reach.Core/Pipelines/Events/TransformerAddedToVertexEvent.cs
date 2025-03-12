namespace Reach.Pipelines.Events;

[GenerateSerializer]
public class TransformerAddedToVertexEvent : BasePipelineEvent
{
    public TransformerAddedToVertexEvent(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid NodeId { get; set; }

    [Id(1)]
    public Guid VertexId { get; set; }

    [Id(2)]
    public Guid TransformerId { get; set; }

    [Id(3)]
    public string TransformerType { get; set; } = null!;

    [Id(4)]
    public string? TransformerParams { get; set; }

    [Id(5)]
    public int Order { get; set; }
}
