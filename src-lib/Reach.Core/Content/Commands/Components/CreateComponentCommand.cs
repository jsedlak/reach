using Reach.Cqrs;

namespace Reach.Content.Commands.Components;

[GenerateSerializer]
public class CreateComponentCommand : AggregateCommand
{
    public CreateComponentCommand(Guid organizationId, Guid aggregateId, Guid hubId)
        : base(organizationId, aggregateId, hubId)
    {
    }
}