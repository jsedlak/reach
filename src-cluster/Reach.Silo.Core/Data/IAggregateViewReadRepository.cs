using System.Linq.Expressions;
using Reach.Cqrs;

namespace Reach.Silo.Data;

public interface IAggregateViewReadRepository<TView>
    where TView : IAggregateView
{
    Task<IQueryable<TView>> Query(Guid organizationId, Guid hubId);

    Task<IQueryable<TView>> Query(Guid organizationId, Guid hubId, Expression<Func<TView, bool>> predicate);

    Task<TView?> Get(Guid organizationId, Guid hubId, Guid viewId);
}