using Reach.Cqrs;

namespace Reach.Pipelines.Commands;

[GenerateSerializer]
public class RenamePipelineCommand : AggregateCommand
{
    public RenamePipelineCommand(ResourceId aggregateId) 
        : base(aggregateId)
    {
    }

    public RenamePipelineCommand(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;
}
