using Reach.Cqrs;

namespace Reach.Content.Commands.Components;

[GenerateSerializer]
public class SetComponentFieldValueCommand : AggregateCommand
{
    public SetComponentFieldValueCommand(Guid organizationId, Guid aggregateId, Guid hubId)
        : base(organizationId, aggregateId, hubId)
    {
    }
}