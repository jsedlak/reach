using Reach.Cqrs;

namespace Reach.Content.Events.Fields;

public abstract class BaseFieldDefinitionEvent : BaseEvent
{
    protected BaseFieldDefinitionEvent(Guid aggregateId) 
        : base(aggregateId)
    {
    }
}
