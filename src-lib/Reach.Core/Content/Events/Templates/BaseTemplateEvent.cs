using Reach.Cqrs;

namespace Reach.Content.Events.Templates;

[GenerateSerializer]
public abstract class BaseTemplateEvent : BaseEvent
{
    protected BaseTemplateEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}
