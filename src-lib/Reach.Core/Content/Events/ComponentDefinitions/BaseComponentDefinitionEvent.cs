using Reach.Cqrs;

namespace Reach.Content.Events.ComponentDefinitions;

[GenerateSerializer]
public abstract class BaseComponentDefinitionEvent : BaseEvent
{
    public BaseComponentDefinitionEvent(Guid aggregateId, Guid organizationId, Guid hubId)
        : base(aggregateId, organizationId, hubId)
    {
    }

}