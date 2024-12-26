using Reach.Content.Views;

namespace Reach.Content.ServiceModel;

public interface IEditorDefinitionViewReadRepository
{
    Task<IQueryable<EditorDefinitionView>> Query();


    Task<IQueryable<EditorDefinitionView>> Query(Guid organizationId, Guid hubId);

    Task<EditorDefinitionView?> Get(Guid id);
}
