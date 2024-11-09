namespace Reach.Content.Model;

/// <summary>
/// Provides a way to customize each editor through a basic set of properties
/// </summary>
public class EditorParameterDefinition
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public EditorParameterType Type { get; set; }

}
