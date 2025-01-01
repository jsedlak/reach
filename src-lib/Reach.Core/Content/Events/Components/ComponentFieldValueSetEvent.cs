namespace Reach.Content.Events.Components;

[GenerateSerializer]
public class ComponentFieldValueSetEvent : BaseComponentEvent
{
    public ComponentFieldValueSetEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid FieldId { get; set; }

    [Id(1)]
    public string Value { get; set; } = string.Empty;
}