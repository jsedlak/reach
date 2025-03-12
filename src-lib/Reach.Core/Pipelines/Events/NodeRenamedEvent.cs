namespace Reach.Pipelines.Events;

[GenerateSerializer]
public class NodeRenamedEvent : BasePipelineEvent
{
    public NodeRenamedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid NodeId { get; set; }
    
    [Id(1)]
    public string Name { get; set; } = null!;
}