using Reach.Cqrs;

namespace Reach.Content.Commands.RendererDefinitions;

[GenerateSerializer]
public class RenameRendererDefinitionCommand : AggregateCommand
{
    public RenameRendererDefinitionCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {

    }

    public RenameRendererDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Slug { get; set; } = null!;
}