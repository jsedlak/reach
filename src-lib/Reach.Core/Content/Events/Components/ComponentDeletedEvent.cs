namespace Reach.Content.Events.Components;

[GenerateSerializer]
public class ComponentDeletedEvent : BaseComponentEvent
{
    public ComponentDeletedEvent(Guid aggregateId, Guid organizationId, Guid hubId) 
        : base(aggregateId, organizationId, hubId)
    {
    }
}