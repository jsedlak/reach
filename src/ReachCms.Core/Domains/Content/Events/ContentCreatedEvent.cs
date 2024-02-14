using Petl;
using ReachCms.Cqrs;

namespace ReachCms.Domains.Content.Events;

public sealed class ContentCreatedEvent : AggregateEvent
{
    public ContentCreatedEvent()
    {
    }

    public ContentCreatedEvent(Guid aggregateId) 
        : base(aggregateId)
    {
    }

    public Guid ParentId { get; set; }

    public Guid TemplateId { get; set; }

    public string Name { get; set; } = null!;
}

public sealed class ContentRenamedEvent : AggregateEvent
{
    public ContentRenamedEvent()
    {
    }

    public ContentRenamedEvent(Guid aggregateId) : base(aggregateId)
    {
    }

    public string Name { get; set; } = null!;
}