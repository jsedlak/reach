namespace Reach.Pipelines.Events;

[GenerateSerializer]
public class PipelineCreatedEvent : BasePipelineEvent
{
    public PipelineCreatedEvent(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;
}
