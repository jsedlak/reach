using Reach.Cqrs;

namespace Reach.Pipelines.Commands;

[GenerateSerializer]
public class AddNodeToPipelineCommand : AggregateCommand
{
    public AddNodeToPipelineCommand(AggregateId aggregateId) 
        : base(aggregateId)
    {
    }

    public AddNodeToPipelineCommand(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;
}
