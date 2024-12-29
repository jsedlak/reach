namespace Reach.Content.Events.ComponentDefinitions;

public class FieldAddedToComponentDefinition : BaseComponentDefinitionEvent
{
    public FieldAddedToComponentDefinition(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}