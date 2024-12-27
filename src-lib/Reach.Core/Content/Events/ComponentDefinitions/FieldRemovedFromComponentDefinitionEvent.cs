namespace Reach.Content.Events.ComponentDefinitions;

[GenerateSerializer]
public class FieldRemovedFromComponentDefinitionEvent : BaseComponentDefinitionEvent
{
    public FieldRemovedFromComponentDefinitionEvent(Guid aggregateId, Guid organizationId, Guid hubId) 
        : base(aggregateId, organizationId, hubId)
    {
    }
}