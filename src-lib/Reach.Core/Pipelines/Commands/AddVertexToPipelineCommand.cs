using Reach.Cqrs;

namespace Reach.Pipelines.Commands;

[GenerateSerializer]
public class AddVertexToPipelineCommand : AggregateCommand
{
    public AddVertexToPipelineCommand(AggregateId aggregateId) 
        : base(aggregateId)
    {
    }

    public AddVertexToPipelineCommand(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid SourceNodeId { get; set; }

    [Id(1)]
    public Guid DestinationNodeId { get; set; }
}
