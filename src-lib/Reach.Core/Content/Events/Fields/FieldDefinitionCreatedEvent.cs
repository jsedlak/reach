
using Reach.Content.Model;

namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public class FieldDefinitionCreatedEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionCreatedEvent(Guid aggregateId) 
        : base(aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Key { get; set; } = null!;

    [Id(2)]
    public Guid EditorDefinitionId { get; set; }

    [Id(3)]
    public EditorParameter[] EditorParameters { get; set; } = Array.Empty<EditorParameter>();
}
