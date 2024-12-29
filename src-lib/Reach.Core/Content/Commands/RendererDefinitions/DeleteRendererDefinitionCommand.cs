using Reach.Cqrs;

namespace Reach.Content.Commands.RendererDefinitions;

[GenerateSerializer]
public class DeleteRendererDefinitionCommand : AggregateCommand
{
    public DeleteRendererDefinitionCommand() 
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {

    }

    public DeleteRendererDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}
