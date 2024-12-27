using Reach.Cqrs;

namespace Reach.Content.Commands.ComponentDefinitions;

[GenerateSerializer]
public class DeleteComponentDefinitionCommand : AggregateCommand
{
    public DeleteComponentDefinitionCommand(Guid organizationId, Guid aggregateId, Guid hubId)
        : base(organizationId, aggregateId, hubId)
    {
    }
}