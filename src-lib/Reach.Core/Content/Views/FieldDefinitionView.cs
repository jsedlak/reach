namespace Reach.Content.Views;

public class FieldDefinitionView
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Key { get; set; } = string.Empty;

    public Guid EditorDefinitionId { get; set; }

    public Dictionary<string, string> EditorParameters { get; set; } = new();

    public EditorDefinitionView EditorDefinition { get; set; } = new();
}
