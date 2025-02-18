using Reach.Cqrs;

namespace Reach.Content.Commands.FieldDefinitions;

[GenerateSerializer]
public class SetFieldDefinitionEditorCommand : AggregateCommand
{
    public SetFieldDefinitionEditorCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }
    public SetFieldDefinitionEditorCommand(AggregateId aggregateId)
        : base(aggregateId.OrganizationId, aggregateId.HubId, aggregateId.ResourceId)
    {

    }

    public SetFieldDefinitionEditorCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid EditorDefinitionId { get; set; }
}
