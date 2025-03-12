using Reach.Cqrs;

namespace Reach.Content.Commands.EditorDefinitions;

[GenerateSerializer]
public class DeleteEditorDefinitionCommand : AggregateCommand
{
    public DeleteEditorDefinitionCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public DeleteEditorDefinitionCommand(ResourceId resourceId)
        : base(resourceId.OrganizationId, resourceId.HubId, resourceId.AggregateId)
    {

    }

    public DeleteEditorDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}