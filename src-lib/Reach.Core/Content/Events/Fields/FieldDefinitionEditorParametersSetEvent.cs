namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public class FieldDefinitionEditorParametersSetEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionEditorParametersSetEvent(Guid aggregateId)
        : base(aggregateId)
    {
    }

    [Id(0)]
    public Dictionary<string, string> EditorParameters { get; set; } = new();
}