using Reach.Cqrs;

namespace Reach.Content.Commands.ComponentDefinitions;

[GenerateSerializer]
public class CreateComponentDefinitionCommand : AggregateCommand
{
    public CreateComponentDefinitionCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public CreateComponentDefinitionCommand(AggregateId aggregateId) 
        : base(aggregateId.OrganizationId, aggregateId.HubId, aggregateId.ResourceId)
    {

    }
    
    public CreateComponentDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = string.Empty;

    [Id(1)]
    public string Slug { get; set; } = string.Empty;

    [Id(2)]
    public Guid ParentId { get; set; }
}