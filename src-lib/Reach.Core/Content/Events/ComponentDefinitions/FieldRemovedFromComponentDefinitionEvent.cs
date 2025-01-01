namespace Reach.Content.Events.ComponentDefinitions;

[GenerateSerializer]
public class FieldRemovedFromComponentDefinitionEvent : BaseComponentDefinitionEvent
{
    public FieldRemovedFromComponentDefinitionEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid FieldId { get; set; }
}