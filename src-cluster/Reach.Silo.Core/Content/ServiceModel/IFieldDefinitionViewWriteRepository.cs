using Reach.Content.Model;
using Reach.Content.Views;

namespace Reach.Silo.Content.ServiceModel;

public interface IFieldDefinitionViewWriteRepository
{
    Task Upsert(FieldDefinitionView fieldDefinitionView);

    Task Delete(Guid id);
}
