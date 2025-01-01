using Reach.Content.Model;

namespace Reach.Content.Events.ComponentDefinitions;

[GenerateSerializer]
public class FieldAddedToComponentDefinition : BaseComponentDefinitionEvent
{
    public FieldAddedToComponentDefinition(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
    
    [Id(0)]
    public Field Field { get; set; } = null!;
}