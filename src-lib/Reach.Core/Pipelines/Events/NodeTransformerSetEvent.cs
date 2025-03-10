namespace Reach.Pipelines.Events;

[GenerateSerializer]
public class NodeTransformerSetEvent : BasePipelineEvent
{
    public NodeTransformerSetEvent(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid NodeId { get; set; }

    [Id(1)]
    public string TransformerType { get; set; } = null!;

    [Id(2)]
    public string? TransformerParams { get; set; }
}
