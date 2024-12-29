using Reach.Cqrs;

namespace Reach.Content.Views;

public class RendererDefinitionView : BaseAggregateView
{
    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;
}