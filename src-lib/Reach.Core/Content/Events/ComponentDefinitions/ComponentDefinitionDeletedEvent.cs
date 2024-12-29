namespace Reach.Content.Events.ComponentDefinitions;

[GenerateSerializer]
public class ComponentDefinitionDeletedEvent : BaseComponentDefinitionEvent
{
    public ComponentDefinitionDeletedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}