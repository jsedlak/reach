namespace Reach.Pipelines.Views;

[GenerateSerializer]
public class PipelineNodeView
{
    [Id(0)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Id(1)]
    public string Name { get; set; } = PropertyDefaults.ERROR_NO_NAME;

    [Id(2)]
    public bool IsEntry { get; set; }

    [Id(3)]
    public PipelineVertexView[] Vertices { get; set; } = Array.Empty<PipelineVertexView>();

    [Id(4)]
    public string? TransformerType { get; set; } = PropertyDefaults.ERROR_NO_TYPE;

    [Id(5)]
    public string? TransformerParams { get; set; }
}
