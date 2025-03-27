using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Reach.Pipelines.Views;
using Reach.Silo.ConfigModel;
using Reach.Silo.Pipelines.ServiceModel;

namespace Reach.Silo.Pipelines.Services;

internal class MongoPipelineViewRepository : MongoAggregateViewRepository<PipelineView>,
    IPipelineViewWriteRepository, IPipelineViewReadRepository
{
    private const string CollectionName = "pipelines";

    public MongoPipelineViewRepository(IMongoClient mongoClient, IOptions<MongoViewRepositoryOptions> options)
        : base(mongoClient, options.Value.Database, CollectionName)
    {

    }
}