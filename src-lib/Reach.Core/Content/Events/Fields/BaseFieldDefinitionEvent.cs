using Reach.Cqrs;

namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public abstract class BaseFieldDefinitionEvent : BaseEvent
{
    protected BaseFieldDefinitionEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}
