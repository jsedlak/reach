using Reach.Content.Model;
using Reach.Cqrs;

namespace Reach.Content.Commands.EditorDefinitions;

[GenerateSerializer]
public class SetEditorDefinitionParametersCommand : AggregateCommand
{
    public SetEditorDefinitionParametersCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public SetEditorDefinitionParametersCommand(AggregateId aggregateId)
        : base(aggregateId.OrganizationId, aggregateId.HubId, aggregateId.ResourceId)
    {

    }

    public SetEditorDefinitionParametersCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public EditorParameterDefinition[] Parameters { get; set; } = Array.Empty<EditorParameterDefinition>();
}
