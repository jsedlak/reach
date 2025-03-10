using Reach.Cqrs;

namespace Reach.Pipelines.Commands;

[GenerateSerializer]
public class DeletePipelineCommand : AggregateCommand
{
    public DeletePipelineCommand(AggregateId aggregateId) 
        : base(aggregateId)
    {
    }

    public DeletePipelineCommand(Guid organizationId, Guid hubId, Guid aggregateId) 
        : base(organizationId, hubId, aggregateId)
    {
    }
}