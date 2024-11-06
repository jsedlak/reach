namespace Reach.Content.Model;

/// <summary>
/// Describes a type of field that can be used in the platform. For example
/// fields may include a Text Field, Image, Link, or more.
/// </summary>
public class FieldDefinition
{
    /// <summary>
    /// Gets or Sets the unique identifier for this definition
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or Sets the display name for this field definition
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or Sets a slug version of the name
    /// </summary>
    public string Key { get; set; } = null!;

    /// <summary>
    /// Gets or Sets the identifier for the data that defines the editor for managing this field's data
    /// </summary>
    public Guid EditorDefinitionId { get; set; }

    /// <summary>
    /// Gets or Sets the set of parameters and values to pass to the editor component
    /// </summary>
    public Dictionary<string, string> EditorParameters { get; set; } = new();
}
