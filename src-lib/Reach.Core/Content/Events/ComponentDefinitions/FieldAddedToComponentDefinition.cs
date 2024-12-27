namespace Reach.Content.Events.ComponentDefinitions;

public class FieldAddedToComponentDefinition : BaseComponentDefinitionEvent
{
    public FieldAddedToComponentDefinition(Guid aggregateId, Guid organizationId, Guid hubId) 
        : base(aggregateId, organizationId, hubId)
    {
    }
}