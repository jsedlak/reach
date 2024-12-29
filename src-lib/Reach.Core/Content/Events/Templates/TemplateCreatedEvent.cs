namespace Reach.Content.Events.Templates;

[GenerateSerializer]
public class TemplateCreatedEvent : BaseTemplateEvent
{
    public TemplateCreatedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;
}
