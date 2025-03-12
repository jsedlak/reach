using Reach.Cqrs;

namespace Reach.Content.Commands.FieldDefinitions;

[GenerateSerializer]
public class DeleteFieldDefinitionCommand : AggregateCommand
{
    public DeleteFieldDefinitionCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public DeleteFieldDefinitionCommand(ResourceId resourceId)
        : base(resourceId.OrganizationId, resourceId.HubId, resourceId.AggregateId)
    {

    }

    public DeleteFieldDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}