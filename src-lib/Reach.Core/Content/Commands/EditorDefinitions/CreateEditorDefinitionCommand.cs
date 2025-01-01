using Reach.Cqrs;

namespace Reach.Content.Commands.EditorDefinitions;

[GenerateSerializer]
public class CreateEditorDefinitionCommand : AggregateCommand
{
    public CreateEditorDefinitionCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
        
    }
    
    public CreateEditorDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string EditorType { get; set; } = null!;
}
