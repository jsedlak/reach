using Reach.Cqrs;

namespace Reach.Content.Commands.Editors;

[GenerateSerializer]
public class CreateEditorDefinitionCommand : AggregateCommand
{
    public CreateEditorDefinitionCommand()
        : base(Guid.NewGuid())
    {
    }

    public CreateEditorDefinitionCommand(Guid aggregateId) 
        : base(aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string EditorType { get; set; } = null!;
}
