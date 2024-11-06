using Reach.Cqrs;

namespace Reach.Content.Commands.Fields;

[GenerateSerializer]
public class SetFieldDefinitionEditorCommand : AggregateRootCommand
{
    public SetFieldDefinitionEditorCommand(Guid aggregateRootId) 
        : base(aggregateRootId)
    {
    }

    [Id(0)]
    public Guid EditorDefinitionId { get; set; }
}
