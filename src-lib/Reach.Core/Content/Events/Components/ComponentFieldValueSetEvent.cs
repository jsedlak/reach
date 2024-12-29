namespace Reach.Content.Events.Components;

[GenerateSerializer]
public class ComponentFieldValueSetEvent : BaseComponentEvent
{
    public ComponentFieldValueSetEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}