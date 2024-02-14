namespace ReachCms.Cqrs;

public abstract class AggregateEvent : IAggregateEvent
{
    public AggregateEvent() { }

    public AggregateEvent(Guid aggregateId) : this(aggregateId, DateTimeOffset.UtcNow) { }
    public AggregateEvent(Guid aggregateId, DateTimeOffset timestamp)
    {
        AggregateId = aggregateId;
        Timestamp = timestamp;
    }

    public Guid AggregateId { get; set; }

    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}
