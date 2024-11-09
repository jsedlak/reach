using Reach.Content.Model;
using Reach.Cqrs;

namespace Reach.Content.Views;

public class EditorDefinitionView : IView
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string EditorType { get; set; } = string.Empty;

    public EditorParameterDefinition[] Parameters { get; set; } = Array.Empty<EditorParameterDefinition>();
}
