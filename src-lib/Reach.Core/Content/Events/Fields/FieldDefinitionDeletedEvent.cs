
namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public class FieldDefinitionDeletedEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionDeletedEvent(Guid aggregateId) 
        : base(aggregateId)
    {
    }
}