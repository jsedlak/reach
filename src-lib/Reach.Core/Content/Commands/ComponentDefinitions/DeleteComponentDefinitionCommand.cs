using Reach.Cqrs;

namespace Reach.Content.Commands.ComponentDefinitions;

[GenerateSerializer]
public class DeleteComponentDefinitionCommand : AggregateCommand
{
    public DeleteComponentDefinitionCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public DeleteComponentDefinitionCommand(ResourceId resourceId)
        : base(resourceId.OrganizationId, resourceId.HubId, resourceId.AggregateId)
    {

    }

    public DeleteComponentDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}