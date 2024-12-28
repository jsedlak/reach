using Reach.Cqrs;

namespace Reach.Content.Commands.RendererDefinitions;

[GenerateSerializer]
public class DeleteRendererDefinitionCommand : AggregateCommand
{
    public DeleteRendererDefinitionCommand() 
        : base(Guid.Empty, Guid.NewGuid(), Guid.Empty)
    {

    }

    public DeleteRendererDefinitionCommand(Guid organizationId, Guid aggregateId, Guid hubId)
        : base(organizationId, aggregateId, hubId)
    {
    }
}
