using Reach.Cqrs;

namespace Reach.Content.Commands.Fields;

[GenerateSerializer]
public class DeleteFieldDefinitionCommand : AggregateRootCommand
{
    public DeleteFieldDefinitionCommand(Guid aggregateRootId) 
        : base(aggregateRootId)
    {
    }
}