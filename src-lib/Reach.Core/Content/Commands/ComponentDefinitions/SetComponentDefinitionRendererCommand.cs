using Reach.Cqrs;

namespace Reach.Content.Commands.ComponentDefinitions;

[GenerateSerializer]
public class SetComponentDefinitionRendererCommand : AggregateCommand
{
    public SetComponentDefinitionRendererCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }
    
    public SetComponentDefinitionRendererCommand(ResourceId resourceId)
        : base(resourceId.OrganizationId, resourceId.HubId, resourceId.AggregateId)
    {

    }

    public SetComponentDefinitionRendererCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}