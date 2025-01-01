using Reach.Content.Model;
using Reach.Cqrs;

namespace Reach.Content.Commands.FieldDefinitions;

[GenerateSerializer]
public class SetFieldDefinitionEditorParametersCommand : AggregateCommand
{
    public SetFieldDefinitionEditorParametersCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }
    
    public SetFieldDefinitionEditorParametersCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public EditorParameter[] EditorParameters { get; set; } = Array.Empty<EditorParameter>();
}