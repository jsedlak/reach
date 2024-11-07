using Reach.Cqrs;

namespace Reach.Content.Commands.Fields;

[GenerateSerializer]
public class CreateFieldDefinitionCommand : AggregateCommand
{
    public CreateFieldDefinitionCommand(Guid aggregateId) 
        : base(aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public Guid EditorDefinitionId { get; set; }
}
