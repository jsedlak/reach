namespace Reach.Content.Model;

/// <summary>
/// Describes a template for a <see cref="Component"/>
/// </summary>
public class ComponentDefinition
{
    public Guid Id { get; set; } = new Guid();

    public string Name { get; set; } = "Untitled Component";

    public Guid ParentId { get; set; } = Guid.Empty;

    public FieldDefinition[] Fields { get; set; } = Array.Empty<FieldDefinition>();
}