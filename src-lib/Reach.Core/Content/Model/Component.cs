namespace Reach.Content.Model;

public class Component
{
    public Guid Id { get; set; } = new Guid();

    public Guid DefinitionId { get; set; } = new Guid();

    public ComponentDefinition Definition { get; set; } = new();
}
