using Reach.Cqrs;

namespace Reach.Pipelines.Commands;

[GenerateSerializer]
public class RemoveTransformerFromVertexCommand : AggregateCommand
{
    public RemoveTransformerFromVertexCommand(ResourceId aggregateId) 
        : base(aggregateId)
    {
    }

    public RemoveTransformerFromVertexCommand(Guid organizationId, Guid hubId, Guid aggregateId) 
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