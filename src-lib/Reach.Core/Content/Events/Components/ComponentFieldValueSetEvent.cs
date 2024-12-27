namespace Reach.Content.Events.Components;

[GenerateSerializer]
public class ComponentFieldValueSetEvent : BaseComponentEvent
{
    public ComponentFieldValueSetEvent(Guid aggregateId, Guid organizationId, Guid hubId) 
        : base(aggregateId, organizationId, hubId)
    {
    }
}