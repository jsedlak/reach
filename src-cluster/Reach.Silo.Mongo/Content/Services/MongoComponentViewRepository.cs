using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Reach.Content.Views;
using Reach.Silo.ConfigModel;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Services;

internal class MongoComponentViewRepository : MongoAggregateViewRepository<ComponentView>,
    IComponentViewWriteRepository, IComponentViewReadRepository
{
    private const string CollectionName = "components";

    public MongoComponentViewRepository(IMongoClient mongoClient, IOptions<MongoViewRepositoryOptions> options)
        : base(mongoClient, options.Value.Database, CollectionName)
    {
        
    }
}