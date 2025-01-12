using Reach.Content.Model;
using Reach.Cqrs;
using Reach.GraphQl;

namespace Reach.Content.Views;

[GenerateSerializer]
[GraphQueryName("components")]
public class ComponentView : BaseAggregateView
{
    [Id(0)]
    public string Name { get; set; } = null!;

    [Id(1)]
    public string Slug { get; set; } = null!;

    [Id(2)]
    public Guid DefinitionId { get; set; }

    [Id(3)]
    public Field[] Fields { get; set; } = Array.Empty<Field>();
}