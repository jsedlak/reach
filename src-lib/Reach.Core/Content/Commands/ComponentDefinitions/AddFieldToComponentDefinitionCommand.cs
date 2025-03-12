using Reach.Cqrs;

namespace Reach.Content.Commands.ComponentDefinitions;

[GenerateSerializer]
public class AddFieldToComponentDefinitionCommand : AggregateCommand
{
    public AddFieldToComponentDefinitionCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
        
    }

    public AddFieldToComponentDefinitionCommand(ResourceId resourceId)
        : base(resourceId.OrganizationId, resourceId.HubId, resourceId.AggregateId)
    {

    }

    public AddFieldToComponentDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Slug { get; set; } = null!;

    [Id(2)]
    public Guid FieldDefinitionId { get; set; }
}