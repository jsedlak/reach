namespace Reach.Cqrs;

/// <summary>
/// Provides a base implementation for all commands going against an aggregate root
/// </summary>
public abstract class AggregateRootCommand
{
    protected AggregateRootCommand(Guid aggregateRootId)
    {
        Id = aggregateRootId;
    }

    /// <summary>
    /// The unique identifier of the aggregate root being targeted
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
}
