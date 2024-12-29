
namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public class FieldDefinitionDeletedEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionDeletedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}