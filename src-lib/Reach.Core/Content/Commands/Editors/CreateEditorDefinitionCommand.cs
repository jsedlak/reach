using Reach.Cqrs;

namespace Reach.Content.Commands.Editors;

[GenerateSerializer]
public class CreateEditorDefinitionCommand : AggregateCommand
{
    public CreateEditorDefinitionCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
        
    }
    
    public CreateEditorDefinitionCommand(Guid organizationId, Guid aggregateId, Guid hubId) 
        : base(organizationId, aggregateId, hubId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string EditorType { get; set; } = null!;
}
