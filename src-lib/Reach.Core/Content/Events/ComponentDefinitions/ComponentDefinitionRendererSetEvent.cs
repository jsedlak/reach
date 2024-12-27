namespace Reach.Content.Events.ComponentDefinitions;

[GenerateSerializer]
public class ComponentDefinitionRendererSetEvent : BaseComponentDefinitionEvent
{
    public ComponentDefinitionRendererSetEvent(Guid aggregateId, Guid organizationId, Guid hubId) 
        : base(aggregateId, organizationId, hubId)
    {
    }
}