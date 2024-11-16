using Reach.Content.Model;

namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public class FieldDefinitionEditorParametersSetEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionEditorParametersSetEvent(Guid aggregateId)
        : base(aggregateId)
    {
    }

    [Id(0)]
    public EditorParameter[] EditorParameters { get; set; } = Array.Empty<EditorParameter>();
}