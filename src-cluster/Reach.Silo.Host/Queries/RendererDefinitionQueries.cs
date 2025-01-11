using Reach.Content.Views;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Host.Queries;

[ExtendObjectType("Query")]
public class RendererDefinitionQueries
{
    public Task<IQueryable<RendererDefinitionView>> RendererDefinitions(
        [GlobalState] string organizationId,
        [GlobalState] string hubId,
        [Service] IRendererDefinitionViewReadRepository repository) =>
        repository.Query(Guid.Parse(organizationId), Guid.Parse(hubId));
}
