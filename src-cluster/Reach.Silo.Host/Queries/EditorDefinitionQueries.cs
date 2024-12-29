using Reach.Content.ServiceModel;
using Reach.Content.Views;

namespace Reach.Silo.Host.Queries;

[ExtendObjectType("Query")]
public class EditorDefinitionQueries
{
    public Task<IQueryable<EditorDefinitionView>> EditorDefinitions(
        [GlobalState] string organizationId,
        [GlobalState] string hubId,
        [Service] IEditorDefinitionViewReadRepository repository) =>
        repository.Query(Guid.Parse(organizationId), Guid.Parse(hubId));
}