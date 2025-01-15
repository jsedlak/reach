using Reach.Content.Views;

namespace Reach.Platform.ServiceModel;

public interface IEditorService
{
    Task<IEnumerable<EditorView>> GetEditors();
}