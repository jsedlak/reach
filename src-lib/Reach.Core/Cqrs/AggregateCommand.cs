namespace Reach.Cqrs;

/// <summary>
/// Provides a base implementation for all commands going against an aggregate root
/// </summary>
[GenerateSerializer]
public abstract class AggregateCommand
{
    protected AggregateCommand(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }

    /// <summary>
    /// The unique identifier of the aggregate root being targeted
    /// </summary>
    [Id(0)]
    public Guid AggregateId { get; set; } = Guid.NewGuid();

    [Id(1)]
    public Guid TenantId { get; set; } = Guid.Empty;
}
