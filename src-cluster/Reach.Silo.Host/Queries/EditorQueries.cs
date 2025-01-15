using Reach.Content.Views;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Host.Queries;

[ExtendObjectType("Query")]
public class EditorQueries
{
    public Task<IEnumerable<EditorView>> Editors([Service] IEditorViewReadRepository repository) 
        => repository.GetEditors();
}