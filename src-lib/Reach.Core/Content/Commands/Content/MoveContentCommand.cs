using Reach.Cqrs;

namespace Reach.Content.Commands.Content;

[GenerateSerializer]
public class MoveContentCommand : AggregateCommand
{
    public MoveContentCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }
    
    public MoveContentCommand(Guid organizationId, Guid aggregateId, Guid hubId)
        : base(organizationId, aggregateId, hubId)
    {
    }
    
    [Id(0)]
    public Guid TargetHubId { get; set; }

    [Id(1)] public Guid TargetParentId { get; set; }
}