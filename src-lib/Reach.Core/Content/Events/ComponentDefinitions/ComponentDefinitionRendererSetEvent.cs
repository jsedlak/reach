namespace Reach.Content.Events.ComponentDefinitions;

[GenerateSerializer]
public class ComponentDefinitionRendererSetEvent : BaseComponentDefinitionEvent
{
    public ComponentDefinitionRendererSetEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}