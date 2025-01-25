using Reach.Content.Model;
using Reach.Cqrs;

namespace Reach.Content.Commands.EditorDefinitions;

[GenerateSerializer]
public class AddParameterToEditorDefinitionCommand : AggregateCommand
{
    public AddParameterToEditorDefinitionCommand() 
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {

    }

    public AddParameterToEditorDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(1)]
    public string DisplayName { get; set; } = null!;

    [Id(3)]
    public EditorParameterType Type { get; set; }
}
