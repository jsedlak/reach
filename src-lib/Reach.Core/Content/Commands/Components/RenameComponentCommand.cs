using Reach.Cqrs;

namespace Reach.Content.Commands.Components;

[GenerateSerializer]
public class RenameComponentCommand : AggregateCommand
{
    public RenameComponentCommand(Guid organizationId, Guid aggregateId, Guid hubId)
        : base(organizationId, aggregateId, hubId)
    {
    }
}