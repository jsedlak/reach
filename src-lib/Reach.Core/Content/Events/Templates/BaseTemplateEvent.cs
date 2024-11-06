using Reach.Cqrs;

namespace Reach.Content.Events.Templates;

public abstract class BaseTemplateEvent : BaseEvent
{
    protected BaseTemplateEvent(Guid aggregateId) 
        : base(aggregateId)
    {
    }
}
