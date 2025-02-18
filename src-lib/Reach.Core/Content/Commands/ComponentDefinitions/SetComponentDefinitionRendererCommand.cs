using Reach.Cqrs;

namespace Reach.Content.Commands.ComponentDefinitions;

[GenerateSerializer]
public class SetComponentDefinitionRendererCommand : AggregateCommand
{
    public SetComponentDefinitionRendererCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }
    
    public SetComponentDefinitionRendererCommand(AggregateId aggregateId)
        : base(aggregateId.OrganizationId, aggregateId.HubId, aggregateId.ResourceId)
    {

    }

    public SetComponentDefinitionRendererCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}