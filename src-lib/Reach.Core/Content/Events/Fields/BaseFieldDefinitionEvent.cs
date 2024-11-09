using Reach.Cqrs;

namespace Reach.Content.Events.Fields;

[GenerateSerializer]
public abstract class BaseFieldDefinitionEvent : BaseEvent
{
    protected BaseFieldDefinitionEvent(Guid aggregateId) 
        : base(aggregateId)
    {
    }
}
