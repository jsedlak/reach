using Reach.Cqrs;

namespace Reach.Content.Commands.FieldDefinitions;

[GenerateSerializer]
public class CreateFieldDefinitionCommand : AggregateCommand
{
    public CreateFieldDefinitionCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public CreateFieldDefinitionCommand(AggregateId aggregateId)
        : base(aggregateId.OrganizationId, aggregateId.HubId, aggregateId.ResourceId)
    {

    }

    public CreateFieldDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)] public string Name { get; set; } = null!;

    [Id(1)] public string Slug { get; set; } = null!;

    [Id(2)] public Guid EditorDefinitionId { get; set; }
}