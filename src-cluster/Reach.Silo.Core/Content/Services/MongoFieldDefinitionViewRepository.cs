using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Reach.Content.ServiceModel;
using Reach.Content.Views;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Services;

public class MongoFieldDefinitionViewRepository : MongoViewRepository<FieldDefinitionView>,
    IFieldDefinitionViewWriteRepository, IFieldDefinitionViewReadRepository
{
    public const string CollectionName = "field_defns";


    public MongoFieldDefinitionViewRepository(IMongoClient mongoClient, IOptions<MongoViewRepositoryOptions> options)
        : base(mongoClient, options.Value.Database, CollectionName)
    {
    }

    public async Task Upsert(FieldDefinitionView view)
    {
        await UpsertAsync(view);
    }

    public async Task Delete(Guid id)
    {
        await DeleteAsync(id);
    }

    public async Task<FieldDefinitionView?> Get(Guid id)
    {
        return await GetAsync(id);
    }

    public async Task<FieldDefinitionView?> Get(string key)
    {
        var result = await QueryAsync(m => m.Key == key);
        return result.FirstOrDefault();
    }

    public async Task<IQueryable<FieldDefinitionView>> Query()
    {
        return await QueryAsync();
    }

    public async Task<IQueryable<FieldDefinitionView>> Query(Guid organizationId, Guid hubId)
    {
        return await QueryAsync(m => m.OrganizationId == organizationId && m.HubId == hubId);
    }

    
}
