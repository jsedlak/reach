using Reach.Cqrs;

namespace Reach.Content.Commands.FieldDefinitions;

[GenerateSerializer]
public class DeleteFieldDefinitionCommand : AggregateCommand
{
    public DeleteFieldDefinitionCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }
    
    public DeleteFieldDefinitionCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}