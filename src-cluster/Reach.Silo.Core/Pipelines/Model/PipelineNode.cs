namespace Reach.Silo.Pipelines.Model;

public class PipelineNode
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsEntry { get; set; }

    public TransformerDescription? Transformer { get; set; }

    public PipelineVertex[] Vertices { get; set; } = Array.Empty<PipelineVertex>();
}
