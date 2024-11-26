
namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public class FieldDefinitionDeletedEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionDeletedEvent(Guid aggregateId, Guid tenantId)
        : base(aggregateId, tenantId)
    {
    }
}