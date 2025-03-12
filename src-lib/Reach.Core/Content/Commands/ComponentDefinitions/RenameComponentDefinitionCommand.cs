using Reach.Cqrs;

namespace Reach.Content.Commands.ComponentDefinitions;

[GenerateSerializer]
public class RenameComponentDefinitionCommand : AggregateCommand
{
    public RenameComponentDefinitionCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }

    public RenameComponentDefinitionCommand(ResourceId resourceId)
        : base(resourceId.OrganizationId, resourceId.HubId, resourceId.AggregateId)
    {

    }

    public RenameComponentDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = string.Empty;

    [Id(1)]
    public string Slug { get; set; } = string.Empty;
}