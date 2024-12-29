using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Reach.Content.ServiceModel;
using Reach.Content.Views;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Services;

public class MongoRendererDefinitionViewRepository : MongoAggregateViewRepository<RendererDefinitionView>,
    IRendererDefinitionViewWriteRepository, IRendererDefinitionViewReadRepository
{
    private const string CollectionName = "renderer_defns";

    public MongoRendererDefinitionViewRepository(IMongoClient mongoClient, IOptions<MongoViewRepositoryOptions> options)
        : base(mongoClient, options.Value.Database, CollectionName)
    {
        
    }
}