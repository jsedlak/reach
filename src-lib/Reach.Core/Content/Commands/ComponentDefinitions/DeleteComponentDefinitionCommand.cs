using Reach.Cqrs;

namespace Reach.Content.Commands.ComponentDefinitions;

[GenerateSerializer]
public class DeleteComponentDefinitionCommand : AggregateCommand
{
    public DeleteComponentDefinitionCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public DeleteComponentDefinitionCommand(AggregateId aggregateId)
        : base(aggregateId.OrganizationId, aggregateId.HubId, aggregateId.ResourceId)
    {

    }

    public DeleteComponentDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}