namespace Reach.Content.Model;

/// <summary>
/// Represents an instance of a <see cref="FieldDefinition"/> combining
/// parameters and data with an underlying definition
/// </summary>
public class Field
{
    /// <summary>
    /// Gets or Sets the unique identifier
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or Sets the underlying definition
    /// </summary>
    public Guid DefinitionId { get; set; }

    /// <summary>
    /// Gets or Sets the value being stored by this instance
    /// </summary>
    public string Value { get; set; } = string.Empty;
}

