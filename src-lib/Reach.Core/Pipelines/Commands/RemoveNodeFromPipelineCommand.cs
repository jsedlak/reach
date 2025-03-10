using Reach.Cqrs;

namespace Reach.Pipelines.Commands;

[GenerateSerializer]
public class RemoveNodeFromPipelineCommand : AggregateCommand
{
    public RemoveNodeFromPipelineCommand(AggregateId aggregateId) 
        : base(aggregateId)
    {
    }

    public RemoveNodeFromPipelineCommand(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid NodeId { get; set; } = Guid.Empty;
}
