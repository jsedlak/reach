using Reach.Cqrs;

namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public abstract class BaseFieldDefinitionEvent : BaseEvent
{
    protected BaseFieldDefinitionEvent(Guid aggregateId, Guid organizationId, Guid hubId)
        : base(aggregateId, organizationId, hubId)
    {
    }
}
