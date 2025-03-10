using Reach.Cqrs;

namespace Reach.Pipelines.Commands;

[GenerateSerializer]
public class SetNodeTransformerCommand : AggregateCommand
{
    public SetNodeTransformerCommand(AggregateId aggregateId) 
        : base(aggregateId)
    {
    }

    public SetNodeTransformerCommand(Guid organizationId, Guid hubId, Guid aggregateId) 
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