namespace Reach.Content.Events.Templates;

[GenerateSerializer]
public class TemplateCreatedEvent : BaseTemplateEvent
{
    public TemplateCreatedEvent(Guid aggregateId, Guid organizationId, Guid hubId)
        : base(aggregateId, organizationId, hubId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;
}
