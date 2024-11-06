using Reach.Cqrs;

namespace Reach.Content.Commands.Fields;

[GenerateSerializer]
public class SetFieldDefinitionEditorParametersCommand : AggregateRootCommand
{
    public SetFieldDefinitionEditorParametersCommand(Guid aggregateRootId) 
        : base(aggregateRootId)
    {
    }

    [Id(0)]
    public Dictionary<string, string> EditorParameters { get; set; } = new();
}