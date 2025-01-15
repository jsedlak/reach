using Reach.Content.Views;
using Reach.Extensions;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Services;

public class StaticEditorViewReadRepository : IEditorViewReadRepository
{
    private static IEnumerable<EditorView> Editors = [
        new EditorView("Text".ToGuid(), "Text", "Reach.Editors.Text.TextEditor, Reach.Editors"),
        new EditorView("Boolean".ToGuid(), "Boolean", "Reach.Editors.Simple.BooleanEditor, Reach.Editors")
    ];

    public Task<IEnumerable<EditorView>> GetEditors()
    {
        return Task.FromResult(Editors);
    }
}
