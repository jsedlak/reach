namespace Reach.Pipelines.Views;

[GenerateSerializer]
public class PipelineView
{
    [Id(0)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Id(1)]
    public string Name { get; set; } = PropertyDefaults.ERROR_NO_NAME;

    [Id(2)]
    public PipelineNodeView[] Nodes { get; set; } = Array.Empty<PipelineNodeView>();
}
