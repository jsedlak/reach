using Reach.Cqrs;

namespace Reach.Content.Commands.ComponentDefinitions;

[GenerateSerializer]
public class RenameComponentDefinitionCommand : AggregateCommand
{
    public RenameComponentDefinitionCommand(Guid organizationId, Guid aggregateId, Guid hubId) 
        : base(organizationId, aggregateId, hubId)
    {
    }
    
    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;
}