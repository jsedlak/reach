using Reach.Cqrs;

namespace Reach.Content.Commands.ComponentDefinitions;

[GenerateSerializer]
public class AddFieldToComponentDefinitionCommand : AggregateCommand
{
    public AddFieldToComponentDefinitionCommand(Guid organizationId, Guid aggregateId, Guid hubId)
        : base(organizationId, aggregateId, hubId)
    {
    }
    
    public Guid FieldId { get; set; }
}