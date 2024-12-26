using Reach.Content.Model;

namespace Reach.Content.Events.Editors;

[GenerateSerializer]
public class EditorDefinitionParametersSetEvent : BaseEditorDefinitionEvent
{
    public EditorDefinitionParametersSetEvent(Guid aggregateId, Guid organizationId, Guid hubId)
        : base(aggregateId, organizationId, hubId)
    {
    }

    [Id(0)]
    public EditorParameterDefinition[] Parameters { get; set; } = Array.Empty<EditorParameterDefinition>();
}
