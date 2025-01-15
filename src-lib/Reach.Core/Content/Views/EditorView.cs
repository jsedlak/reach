using Reach.GraphQl;

namespace Reach.Content.Views;

[GenerateSerializer]
[GraphQueryName("editors")]
public class EditorView
{
    public EditorView()
    {
    }

    public EditorView(Guid id, string name, string typeName)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
    }

    public Guid Id { get; set; } = Guid.Empty;

    public string Name { get; set; } = null!;

    public string TypeName { get; set; } = null!;
}