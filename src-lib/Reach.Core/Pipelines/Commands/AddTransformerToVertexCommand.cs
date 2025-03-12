using Reach.Cqrs;

namespace Reach.Pipelines.Commands;

[GenerateSerializer]
public class AddTransformerToVertexCommand : AggregateCommand
{
    public AddTransformerToVertexCommand(ResourceId aggregateId)
        : base(aggregateId)
    {
    }

    public AddTransformerToVertexCommand(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid NodeId { get; set; }

    [Id(1)]
    public Guid VertexId { get; set; }

    [Id(2)]
    public string TransformerType { get; set; } = null!;

    [Id(3)]
    public string? TransformerParams { get; set; }
}
