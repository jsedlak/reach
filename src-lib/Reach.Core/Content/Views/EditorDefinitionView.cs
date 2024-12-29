using Reach.Content.Model;
using Reach.Cqrs;

namespace Reach.Content.Views;

public class EditorDefinitionView : BaseAggregateView
{
    public string Name { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public string EditorType { get; set; } = string.Empty;

    public EditorParameterDefinition[] Parameters { get; set; } = Array.Empty<EditorParameterDefinition>();
}