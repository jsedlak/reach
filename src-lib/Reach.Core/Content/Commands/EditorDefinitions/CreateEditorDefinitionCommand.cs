using Reach.Cqrs;

namespace Reach.Content.Commands.EditorDefinitions;

[GenerateSerializer]
public class CreateEditorDefinitionCommand : AggregateCommand
{
    public CreateEditorDefinitionCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
        
    }

    public CreateEditorDefinitionCommand(AggregateId aggregateId)
        : base(aggregateId.OrganizationId, aggregateId.HubId, aggregateId.ResourceId)
    {

    }

    public CreateEditorDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Slug { get; set; } = null!;

    [Id(2)]
    public string EditorType { get; set; } = null!;
}
