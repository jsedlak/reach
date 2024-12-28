using Reach.Cqrs;

namespace Reach.Content.Commands.Components;

[GenerateSerializer]
public class CreateComponentCommand : AggregateCommand
{
    public CreateComponentCommand(Guid organizationId, Guid aggregateId, Guid hubId)
        : base(organizationId, aggregateId, hubId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Slug { get; set; } = null!;
}