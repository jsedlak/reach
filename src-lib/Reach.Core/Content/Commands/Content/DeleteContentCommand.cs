using Reach.Cqrs;

namespace Reach.Content.Commands.Content;

[GenerateSerializer]
public class DeleteContentCommand : AggregateCommand
{
    public DeleteContentCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public DeleteContentCommand(Guid organizationId, Guid aggregateId, Guid hubId)
        : base(organizationId, aggregateId, hubId)
    {
    }
}