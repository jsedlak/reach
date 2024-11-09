using Reach.Cqrs;

namespace Reach.Content.Events.Templates;

[GenerateSerializer]
public abstract class BaseTemplateEvent : BaseEvent
{
    protected BaseTemplateEvent(Guid aggregateId) 
        : base(aggregateId)
    {
    }
}
