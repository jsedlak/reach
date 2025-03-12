using Reach.Cqrs;

namespace Reach.Pipelines.Commands;

[GenerateSerializer]
public class RemoveVertexFromPipelineCommand :AggregateCommand
{
    public RemoveVertexFromPipelineCommand(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid SourceNodeId { get; set; }

    [Id(1)] 
    public Guid VertexId { get; set; }
}