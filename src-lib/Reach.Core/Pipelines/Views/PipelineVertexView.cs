namespace Reach.Pipelines.Views;

[GenerateSerializer]
public class PipelineVertexView
{
    [Id(0)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Id(1)]
    public string Name { get; set; } = PropertyDefaults.ERROR_NO_NAME;

    [Id(2)]
    public string? TransformerType { get; set; } = PropertyDefaults.ERROR_NO_TYPE;

    [Id(3)]
    public string? TransformerParams { get; set; }

    [Id(4)]
    public Guid DestinationNodeId { get; set; }
}
