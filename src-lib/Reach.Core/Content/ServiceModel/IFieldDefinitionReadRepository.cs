using Reach.Content.Views;

namespace Reach.Content.ServiceModel;

public interface IFieldDefinitionReadRepository
{
    Task<IQueryable<FieldDefinitionView>> Query();

    Task<FieldDefinitionView> Get(Guid id);

    Task<FieldDefinitionView> Get(string key);
}
