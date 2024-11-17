namespace Reach.Content.Model;

[GenerateSerializer]
public class EditorParameter
{
    [Id(0)]
    public string Key { get; set; } = string.Empty;

    [Id(1)]
    public string Value { get; set; } = string.Empty;
}