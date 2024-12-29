using Reach.Content.ServiceModel;
using Reach.Content.Views;

namespace Reach.Silo.Host.Queries;

[ExtendObjectType("Query")]
public class ContentItemQueries
{
    public Task<IQueryable<ContentItemView>> Content(
        [GlobalState] string organizationId,
        [GlobalState] string hubId,
        [Service] IContentItemViewReadRepository repository) =>
        repository.Query(Guid.Parse(organizationId), Guid.Parse(hubId));
}