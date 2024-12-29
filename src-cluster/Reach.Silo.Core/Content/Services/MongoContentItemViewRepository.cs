using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Reach.Content.ServiceModel;
using Reach.Content.Views;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Services;

public class MongoContentItemViewRepository : MongoAggregateViewRepository<ContentItemView>,
    IContentItemViewWriteRepository, IContentItemViewReadRepository
{
    private const string CollectionName = "content_items";

    public MongoContentItemViewRepository(IMongoClient mongoClient, IOptions<MongoViewRepositoryOptions> options)
        : base(mongoClient, options.Value.Database, CollectionName)
    {
        
    }
}