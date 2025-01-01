using Reach.Cqrs;

namespace Reach.Content.Commands.Components;

[GenerateSerializer]
public class SetComponentFieldValueCommand : AggregateCommand
{
    public SetComponentFieldValueCommand()
        : base(Guid.Empty, Guid.Empty, Guid.NewGuid())
    {
    }
    
    public SetComponentFieldValueCommand(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string? FieldKey { get; set; }

    [Id(1)]
    public Guid? FieldId { get; set; }

    [Id(2)]
    public string Value { get; set; } = string.Empty;
}