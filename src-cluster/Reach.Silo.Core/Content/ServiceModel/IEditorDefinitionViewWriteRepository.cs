using Reach.Content.Views;

namespace Reach.Silo.Content.ServiceModel;

public interface IEditorDefinitionViewWriteRepository
{
    Task Upsert(EditorDefinitionView editorDefinitionView);

    Task Delete(Guid id);
}