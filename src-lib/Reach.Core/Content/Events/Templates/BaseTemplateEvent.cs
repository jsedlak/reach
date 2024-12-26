using Reach.Cqrs;

namespace Reach.Content.Events.Templates;

[GenerateSerializer]
public abstract class BaseTemplateEvent : BaseEvent
{
    protected BaseTemplateEvent(Guid aggregateId, Guid organizationId, Guid hubId)
        : base(aggregateId, organizationId, hubId)
    {
    }
}
