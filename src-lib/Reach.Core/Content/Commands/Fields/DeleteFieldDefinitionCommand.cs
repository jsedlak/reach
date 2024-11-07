using Reach.Cqrs;

namespace Reach.Content.Commands.Fields;

[GenerateSerializer]
public class DeleteFieldDefinitionCommand : AggregateCommand
{
    public DeleteFieldDefinitionCommand(Guid aggregateRootId) 
        : base(aggregateRootId)
    {
    }
}