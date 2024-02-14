using Petl;

namespace ReachCms.Cqrs;

public interface IEventStore<TAggregate> 
    where TAggregate : class
{
    Task<IEnumerable<IAggregateEvent>> GetEventsForAggregateAsync(Guid aggregateId);

    Task AddEventsAsync(Guid aggregateId, IEnumerable<IAggregateEvent> events);
}
