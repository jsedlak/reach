
namespace Reach.Content.Events.Fields;

public class FieldDefinitionCreatedEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionCreatedEvent(Guid aggregateId) 
        : base(aggregateId)
    {
    }

    public string Name { get; set; } = null!;

    public string Key { get; set; } = null!;
    
    public Guid EditorDefinitionId { get; set; }

    public Dictionary<string, string> EditorParameters { get; set; } = new();
}
