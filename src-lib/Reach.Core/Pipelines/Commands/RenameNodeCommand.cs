using Reach.Cqrs;

namespace Reach.Pipelines.Commands;

[GenerateSerializer]
public class RenameNodeCommand : AggregateCommand
{
    public RenameNodeCommand(ResourceId aggregateId)
        : base(aggregateId)
    {
    }
    public RenameNodeCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid NodeId { get; set; } = Guid.Empty;

    [Id(1)]
    public string Name { get; set; } = null!;
}