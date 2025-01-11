using Reach.Content.Views;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Host.Queries;

[ExtendObjectType("Query")]
public class ComponentQueries
{
    public Task<IQueryable<ComponentView>> Components(
        [GlobalState] string organizationId,
        [GlobalState] string hubId,
        [Service] IComponentViewReadRepository repository) =>
        repository.Query(Guid.Parse(organizationId), Guid.Parse(hubId));
}