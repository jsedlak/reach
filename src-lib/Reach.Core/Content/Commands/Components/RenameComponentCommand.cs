using Reach.Cqrs;

namespace Reach.Content.Commands.Components;

[GenerateSerializer]
public class RenameComponentCommand : AggregateCommand
{
    public RenameComponentCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public RenameComponentCommand(ResourceId resourceId)
        : base(resourceId.OrganizationId, resourceId.HubId, resourceId.AggregateId)
    {

    }

    public RenameComponentCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Slug { get; set; } = null!;
}