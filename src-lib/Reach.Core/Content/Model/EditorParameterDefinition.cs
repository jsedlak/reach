namespace Reach.Content.Model;

/// <summary>
/// Provides a way to customize each editor through a basic set of properties
/// </summary>
[GenerateSerializer]
public class EditorParameterDefinition
{
    /// <summary>
    /// Gets or Sets the parameter name on the component object
    /// </summary>
    [Id(0)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or Sets the editor friendly display name
    /// </summary>
    [Id(1)]
    public string DisplayName { get; set; } = null!;

    [Id(2)]
    public string? Description { get; set; }

    [Id(3)]
    public EditorParameterType Type { get; set; }

}
