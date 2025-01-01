using Reach.Content.Model;

namespace Reach.Content.Events.EditorDefinitions;

[GenerateSerializer]
public class EditorDefinitionParametersSetEvent : BaseEditorDefinitionEvent
{
    public EditorDefinitionParametersSetEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public EditorParameterDefinition[] Parameters { get; set; } = Array.Empty<EditorParameterDefinition>();
}
