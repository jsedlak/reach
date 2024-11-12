using Reach.Cqrs;

namespace Reach.Content.Commands.Editors;

[GenerateSerializer]
public class DeleteEditorDefinitionCommand : AggregateCommand
{
    public DeleteEditorDefinitionCommand(Guid aggregateId) 
        : base(aggregateId)
    {
    }
}