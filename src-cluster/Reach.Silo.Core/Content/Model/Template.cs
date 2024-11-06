namespace Reach.Content.Model;

/// <summary>
/// Represents the base from which new <see cref="Content"/> may be created.
/// </summary>
public class Template
{
    /// <summary>
    /// Gets or Sets the unique identifier
    /// </summary>
    public Guid Id { get; set; } = new Guid();

    /// <summary>
    /// Gets or Sets the name of the template
    /// </summary>
    public string Name { get; set; } = "Untitled Template";

    /// <summary>
    /// Gets or Sets the parent unique identifier
    /// </summary>
    public Guid ParentId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or Sets the list of field definitions that should be included in the template
    /// </summary>
    public FieldDefinition[] Fields { get; set; } = Array.Empty<FieldDefinition>();
}
