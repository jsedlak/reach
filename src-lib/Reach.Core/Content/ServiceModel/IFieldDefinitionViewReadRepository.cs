using Reach.Content.Views;

namespace Reach.Content.ServiceModel;

public interface IFieldDefinitionViewReadRepository
{
    Task<IQueryable<FieldDefinitionView>> Query();

    Task<IQueryable<FieldDefinitionView>> Query(Guid organizationId, Guid hubId);

    Task<FieldDefinitionView?> Get(Guid id);

    Task<FieldDefinitionView?> Get(string key);
}
