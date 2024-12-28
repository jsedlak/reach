using Reach.Cqrs;

namespace Reach.Content.Commands.Components;

[GenerateSerializer]
public class RenameComponentCommand : AggregateCommand
{
    public RenameComponentCommand(Guid organizationId, Guid aggregateId, Guid hubId)
        : base(organizationId, aggregateId, hubId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Slug { get; set; } = null!;
}