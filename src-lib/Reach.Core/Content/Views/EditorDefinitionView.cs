using Reach.Content.Model;
using Reach.Cqrs;
using Reach.GraphQl;

namespace Reach.Content.Views;

[GenerateSerializer]
[GraphQueryName("editorDefinitions")]
public class EditorDefinitionView : BaseAggregateView
{
    [Id(0)]
    public string Name { get; set; } = string.Empty;

    [Id(1)]
    public string DisplayName { get; set; } = string.Empty;

    [Id(2)]
    public string EditorType { get; set; } = string.Empty;

    [Id(3)]
    public EditorParameterDefinition[] Parameters { get; set; } = Array.Empty<EditorParameterDefinition>();
}