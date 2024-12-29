namespace Reach.Content.Events.Components;

[GenerateSerializer]
public class ComponentDeletedEvent : BaseComponentEvent
{
    public ComponentDeletedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}