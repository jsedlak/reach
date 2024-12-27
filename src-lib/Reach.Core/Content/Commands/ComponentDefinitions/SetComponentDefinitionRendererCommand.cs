using Reach.Cqrs;

namespace Reach.Content.Commands.ComponentDefinitions;

[GenerateSerializer]
public class SetComponentDefinitionRendererCommand : AggregateCommand
{
    public SetComponentDefinitionRendererCommand(Guid organizationId, Guid aggregateId, Guid hubId)
        : base(organizationId, aggregateId, hubId)
    {
    }
}