
namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public class FieldDefinitionDeletedEvent : BaseFieldDefinitionEvent
{
    public FieldDefinitionDeletedEvent(Guid aggregateId, Guid organizationId, Guid hubId)
        : base(aggregateId, organizationId, hubId)
    {
    }
}