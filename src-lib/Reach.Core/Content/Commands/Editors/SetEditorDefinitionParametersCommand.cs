using Reach.Content.Model;
using Reach.Cqrs;

namespace Reach.Content.Commands.Editors;

[GenerateSerializer]
public class SetEditorDefinitionParametersCommand : AggregateCommand
{
    public SetEditorDefinitionParametersCommand(Guid aggregateId) 
        : base(aggregateId)
    {
    }

    [Id(0)]
    public EditorParameterDefinition[] Parameters { get; set; } = Array.Empty<EditorParameterDefinition>();
}
