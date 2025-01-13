using Reach.Cqrs;
using Reach.GraphQl;

namespace Reach.Content.Views;

[GenerateSerializer]
[GraphQueryName("renderDefinitions")]
public class RendererDefinitionView : BaseAggregateView
{
    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Slug { get; set; } = null!;
}