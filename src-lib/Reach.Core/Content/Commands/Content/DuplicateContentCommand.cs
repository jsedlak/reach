using Reach.Cqrs;

namespace Reach.Content.Commands.Content;

[GenerateSerializer]
public class DuplicateContentCommand : AggregateCommand
{
    public DuplicateContentCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }
    
    public DuplicateContentCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid TargetHubId { get; set; }

    [Id(1)]
    public Guid TargetParentId { get; set; }
}