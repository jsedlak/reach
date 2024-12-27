using Reach.Cqrs;

namespace Reach.Content.Commands.ComponentDefinitions;

[GenerateSerializer]
public class RemoveFieldFromComponentDefinitionCommand : AggregateCommand
{
    public RemoveFieldFromComponentDefinitionCommand(Guid organizationId, Guid aggregateId, Guid hubId)
        : base(organizationId, aggregateId, hubId)
    {
    }
    
    public Guid FieldId { get; set; }
}