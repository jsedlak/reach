using Reach.Content.Views;

namespace Reach.Content.ServiceModel;

public interface IEditorDefinitionViewReadRepository
{
    Task<IQueryable<EditorDefinitionView>> Query();

    Task<EditorDefinitionView> Get(Guid id);
}
