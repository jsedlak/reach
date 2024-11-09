namespace Reach.Content.Events.Templates;

[GenerateSerializer]
public class TemplateCreatedEvent : BaseTemplateEvent
{
    public TemplateCreatedEvent(Guid aggregateId) 
        : base(aggregateId)
    {
    }

    [Id(0)]
    public string Name { get; set; } = null!;
}
