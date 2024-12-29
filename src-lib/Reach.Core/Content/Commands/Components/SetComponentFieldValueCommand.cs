using Reach.Cqrs;

namespace Reach.Content.Commands.Components;

[GenerateSerializer]
public class SetComponentFieldValueCommand : AggregateCommand
{
    public SetComponentFieldValueCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }
    
    public SetComponentFieldValueCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}