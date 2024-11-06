namespace Reach.Content.Events.Templates;

public class TemplateCreatedEvent : BaseTemplateEvent
{
    public TemplateCreatedEvent(Guid aggregateId) 
        : base(aggregateId)
    {
    }

    public string Name { get; set; } = null!;
}
