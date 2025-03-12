namespace Reach.Silo.Pipelines.Model;

public class PipelineVertex
{
    public Guid Id { get; set; }

    public Guid DestinationNodeId { get; set; }
    
    public TransformerDescription[] Transformers { get; set; } = Array.Empty<TransformerDescription>();
}
