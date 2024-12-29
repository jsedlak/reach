using Reach.Cqrs;

namespace Reach.Silo.Content.ServiceModel;

public interface IAggregateViewWriteRepository<TAggregate>
    where TAggregate : class, IAggregateView
{
    
    Task Upsert(TAggregate view);

    Task Delete(Guid organizationId, Guid hubId, Guid viewId);
}