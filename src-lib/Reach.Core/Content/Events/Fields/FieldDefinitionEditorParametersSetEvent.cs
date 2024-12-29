using Reach.Content.Model;

namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public class FieldDefinitionEditorParametersSetEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionEditorParametersSetEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public EditorParameter[] EditorParameters { get; set; } = Array.Empty<EditorParameter>();
}