using Reach.Cqrs;

namespace Reach.Content.Commands.Fields;

[GenerateSerializer]
public class CreateFieldDefinitionCommand : AggregateRootCommand
{
    public CreateFieldDefinitionCommand(Guid aggregateRootId) 
        : base(aggregateRootId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public Guid EditorDefinitionId { get; set; }
}
