namespace Reach.Silo.Content.Model;

public class RendererDefinition
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;
}
