namespace Reach.Content.Events.Templates;

[GenerateSerializer]
public class TemplateCreatedEvent : BaseTemplateEvent
{
    public TemplateCreatedEvent(Guid aggregateId, Guid tenantId)
        : base(aggregateId, tenantId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;
}
