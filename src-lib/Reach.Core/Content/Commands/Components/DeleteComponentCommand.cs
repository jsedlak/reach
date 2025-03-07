using Reach.Cqrs;

namespace Reach.Content.Commands.Components;

[GenerateSerializer]
public class DeleteComponentCommand : AggregateCommand
{
    public DeleteComponentCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public DeleteComponentCommand(AggregateId aggregateId)
        : base(aggregateId.OrganizationId, aggregateId.HubId, aggregateId.ResourceId)
    {

    }

    public DeleteComponentCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}