using Reach.Content.Views;

namespace Reach.Silo.Content.ServiceModel;

public interface IEditorViewReadRepository
{
    Task<IEnumerable<EditorView>> GetEditors();
}
