namespace Reach.Silo.Pipelines.Model;

public class TransformerDescription
{
    public Guid Id { get; set; }

    public int Order { get; set; }

    public string TransformerType { get; set; } = null!;

    public string? TransformerParams { get; set; }
}