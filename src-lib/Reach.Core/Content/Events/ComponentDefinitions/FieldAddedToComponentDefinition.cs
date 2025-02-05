using Reach.Content.Model;

namespace Reach.Content.Events.ComponentDefinitions;

[GenerateSerializer]
public class FieldAddedToComponentDefinitionEvent : BaseComponentDefinitionEvent
{
    public FieldAddedToComponentDefinitionEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
    
    [Id(0)]
    public Field Field { get; set; } = null!;
}