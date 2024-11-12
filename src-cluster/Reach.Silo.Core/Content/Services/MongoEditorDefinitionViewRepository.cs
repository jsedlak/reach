using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Reach.Content.ServiceModel;
using Reach.Content.Views;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Services;

public class MongoEditorDefinitionViewRepository : MongoViewRepository<EditorDefinitionView>, IEditorDefinitionViewReadRepository, IEditorDefinitionViewWriteRepository
{
    public const string CollectionName = "editor_defns";

    public MongoEditorDefinitionViewRepository(IMongoClient mongoClient, IOptions<MongoViewRepositoryOptions> options)
        : base(mongoClient, options.Value.Database, CollectionName)
    {
    }

    public async Task Upsert(EditorDefinitionView editorDefinitionView)
    {
        await UpsertAsync(editorDefinitionView);
    }

    public async Task Delete(Guid id)
    {
        await DeleteAsync(id);
    }

    public async Task<EditorDefinitionView?> Get(Guid id)
    {
        return await GetAsync(id);
    }

    public async Task<IQueryable<EditorDefinitionView>> Query()
    {
        return await QueryAsync();
    }
}
