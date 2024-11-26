using Reach.Cqrs;

namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public abstract class BaseFieldDefinitionEvent : BaseEvent
{
    protected BaseFieldDefinitionEvent(Guid aggregateId, Guid tenantId)
        : base(aggregateId, tenantId)
    {
    }
}
