using Reach.Content.Model;

namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public class FieldDefinitionEditorParametersSetEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionEditorParametersSetEvent(Guid aggregateId, Guid tenantId)
        : base(aggregateId, tenantId)
    {
    }

    [Id(0)]
    public EditorParameter[] EditorParameters { get; set; } = Array.Empty<EditorParameter>();
}