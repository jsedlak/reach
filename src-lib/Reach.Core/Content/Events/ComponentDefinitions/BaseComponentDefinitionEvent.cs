using Reach.Cqrs;

namespace Reach.Content.Events.ComponentDefinitions;

[GenerateSerializer]
public abstract class BaseComponentDefinitionEvent : BaseEvent
{
    public BaseComponentDefinitionEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

}