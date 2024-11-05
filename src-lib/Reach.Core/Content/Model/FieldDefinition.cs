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
}
