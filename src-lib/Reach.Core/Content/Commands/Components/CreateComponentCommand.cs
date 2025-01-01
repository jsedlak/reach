using Reach.Cqrs;

namespace Reach.Content.Commands.Components;

[GenerateSerializer]
public class CreateComponentCommand : AggregateCommand
{
    public CreateComponentCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public CreateComponentCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)] public string Name { get; set; } = null!;

    [Id(1)] public string Slug { get; set; } = null!;

    [Id(2)] public Guid DefinitionId { get; set; }
}