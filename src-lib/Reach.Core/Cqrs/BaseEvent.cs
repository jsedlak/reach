namespace Reach.Cqrs;

/// <summary>
/// Provides a base event class for all events in the platform
/// </summary>
[GenerateSerializer]
public abstract class BaseEvent
{
    protected BaseEvent(Guid aggregateId, Guid tenantId)
    {
        AggregateId = aggregateId;
        TenantId = tenantId;
    }

    /// <summary>
    /// Gets or Sets the aggregate to which this event pertains
    /// </summary>
    [Id(0)]
    public Guid AggregateId { get; set; }

    /// <summary>
    /// Gets or Sets the time at which this event was created
    /// </summary>
    [Id(1)]
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// Gets or Sets the tenant to which this event belongs
    /// </summary>
    [Id(2)]
    public Guid TenantId { get; set; } = Guid.Empty;
}
