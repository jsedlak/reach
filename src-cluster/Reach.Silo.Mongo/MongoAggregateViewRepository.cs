using System.Linq.Expressions;
using MongoDB.Driver;
using Reach.Cqrs;
using Reach.Silo.Data;

namespace Reach.Silo;

/// <summary>
/// Provides a base implementation for reading/querying aggregate views,
/// which are based on entities in the React platform that belong to
/// an organization and hub.
/// </summary>
/// <typeparam name="TAggregate">The type of aggregate view being stored.</typeparam>
internal abstract class MongoAggregateViewRepository<TAggregate> : 
    MongoViewRepository<TAggregate>, 
    IAggregateViewReadRepository<TAggregate>,
    IAggregateViewWriteRepository<TAggregate>
    where TAggregate : class, IAggregateView
{
    protected MongoAggregateViewRepository(IMongoClient mongoClient, string databaseName, string collectionName) 
        : base(mongoClient, databaseName, collectionName)
    {
    }

    public async Task<IQueryable<TAggregate>> Query(Guid organizationId, Guid hubId)
    {
        var result = await QueryAsync(m =>
            m.OrganizationId == organizationId &&
            m.HubId == hubId
        );

        return result;
    }

    public async Task<IQueryable<TAggregate>> Query(Guid organizationId, Guid hubId, Expression<Func<TAggregate, bool>> predicate)
    {
        var baseQueryResult = await QueryAsync(
            m => m.OrganizationId == organizationId && m.HubId == hubId);
        
        return baseQueryResult.Where(predicate);
    }

    public Task<TAggregate?> Get(Guid organizationId, Guid hubId, Guid viewId)
    {
        return GetAsync(m => 
            m.OrganizationId == organizationId &&
            m.HubId == hubId &&
            m.Id == viewId
        );
    }

    public Task Upsert(TAggregate view)
    {
        return UpsertAsync(view);
    }

    public Task Delete(Guid organizationId, Guid hubId, Guid viewId)
    {
        return DeleteAsync(m =>
            m.OrganizationId == organizationId &&
            m.HubId == hubId &&
            m.Id == viewId
        );
    }
}