using Reach.Cqrs;

namespace Reach.Content.Views;

public class ContentItemView : BaseAggregateView
{
    
    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;
}