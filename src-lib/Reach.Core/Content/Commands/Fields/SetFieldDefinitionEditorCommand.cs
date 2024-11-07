using Reach.Cqrs;

namespace Reach.Content.Commands.Fields;

[GenerateSerializer]
public class SetFieldDefinitionEditorCommand : AggregateCommand
{
    public SetFieldDefinitionEditorCommand(Guid aggregateId) 
        : base(aggregateId)
    {
    }

    [Id(0)]
    public Guid EditorDefinitionId { get; set; }
}
