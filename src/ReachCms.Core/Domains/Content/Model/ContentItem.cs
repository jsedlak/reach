using ReachCms.Cqrs;
using ReachCms.Domains.Content.Events;

namespace ReachCms.Domains.Content.Model;

/// <summary>
/// A document describing a piece of content adhering to a particular structure of data (template)
/// </summary>
public class ContentItem : IContentEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ParentId { get; set; } = Guid.Empty;

    public Guid TemplateId { get; set; }

    public string Name { get; set; } = null!;

    public void Apply(IEnumerable<IAggregateEvent> events)
    {
        foreach (var @event in events)
        {
            if(@event is ContentCreatedEvent createdEvent)
            {
                Apply(createdEvent);
            }
        }
    }

    public void Apply(ContentCreatedEvent @event)
    {
        Id = @event.AggregateId;
        ParentId = @event.ParentId;
        TemplateId = @event.TemplateId;
        Name = @event.Name;
    }
}
