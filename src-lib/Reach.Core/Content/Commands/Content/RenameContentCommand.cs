using Reach.Cqrs;

namespace Reach.Content.Commands.Content;

[GenerateSerializer]
public class RenameContentCommand : AggregateCommand
{
    public RenameContentCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public RenameContentCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)] public string Name { get; set; } = string.Empty;

    [Id(1)] public string Slug { get; set; } = string.Empty;
}