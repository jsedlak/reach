namespace Reach.Content.Model;

/// <summary>
/// Defines the interface between the content platform and a renderable block of code
/// </summary>
public class EditorDefinition
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public string EditorType { get; set; } = null!;

    public EditorParameterDefinition[] Parameters { get; set; } = Array.Empty<EditorParameterDefinition>();
}
