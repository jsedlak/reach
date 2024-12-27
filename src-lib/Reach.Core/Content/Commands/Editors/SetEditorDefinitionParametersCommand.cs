using Reach.Content.Model;
using Reach.Cqrs;

namespace Reach.Content.Commands.Editors;

[GenerateSerializer]
public class SetEditorDefinitionParametersCommand : AggregateCommand
{
    public SetEditorDefinitionParametersCommand(Guid organizationId, Guid aggregateId, Guid hubId) 
        : base(organizationId, aggregateId, hubId)
    {
    }

    [Id(0)]
    public EditorParameterDefinition[] Parameters { get; set; } = Array.Empty<EditorParameterDefinition>();
}
