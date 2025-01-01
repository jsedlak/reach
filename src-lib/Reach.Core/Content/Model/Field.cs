namespace Reach.Content.Model;

/// <summary>
/// Represents an instance of a <see cref="FieldDefinition"/> combining
/// parameters and data with an underlying definition
/// </summary>
[GenerateSerializer]
public class Field
{
    public Field()
    {
        
    }

    public Field(Field copy)
    {
        Id = copy.Id;
        Name = copy.Name;
        Slug = copy.Slug;
        DefinitionId = copy.DefinitionId;
        Value = copy.Value;
    }
    
    /// <summary>
    /// Gets or Sets the unique identifier
    /// </summary>
    [Id(0)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Id(1)]
    public string Name { get; set; } = null!;

    [Id(2)]
    public string Slug { get; set; } = null!;

    /// <summary>
    /// Gets or Sets the underlying definition
    /// </summary>
    [Id(3)]
    public Guid DefinitionId { get; set; }

    /// <summary>
    /// Gets or Sets the value being stored by this instance
    /// </summary>
    [Id(4)]
    public string Value { get; set; } = string.Empty;
}

