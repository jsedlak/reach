using Reach.Cqrs;

namespace Reach.Content.Commands.Fields;

[GenerateSerializer]
public class SetFieldDefinitionEditorParametersCommand : AggregateCommand
{
    public SetFieldDefinitionEditorParametersCommand(Guid aggregateId) 
        : base(aggregateId)
    {
    }

    [Id(0)]
    public Dictionary<string, string> EditorParameters { get; set; } = new();
}