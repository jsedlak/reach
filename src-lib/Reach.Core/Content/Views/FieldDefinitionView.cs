using Reach.Content.Model;
using Reach.Cqrs;
using Reach.GraphQl;

namespace Reach.Content.Views;

[GenerateSerializer]
[GraphQueryName("fieldDefinitions")]
public class FieldDefinitionView : BaseAggregateView
{
    [Id(0)]
    public string Name { get; set; } = string.Empty;

    [Id(1)]
    public string Slug { get; set; } = string.Empty;

    [Id(2)]
    public Guid EditorDefinitionId { get; set; }

    [Id(3)]
    public EditorParameter[] EditorParameters { get; set; } = Array.Empty<EditorParameter>();

    [Id(4)]
    public EditorDefinitionView? EditorDefinition { get; set; } = new();
}
