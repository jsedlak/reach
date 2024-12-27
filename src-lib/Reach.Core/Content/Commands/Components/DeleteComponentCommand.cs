using Reach.Cqrs;

namespace Reach.Content.Commands.Components;

[GenerateSerializer]
public class DeleteComponentCommand : AggregateCommand
{
    public DeleteComponentCommand(Guid organizationId, Guid aggregateId, Guid hubId)
        : base(organizationId, aggregateId, hubId)
    {
    }
}