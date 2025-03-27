using Reach.Cqrs;

namespace Reach.Pipelines.Views;

[GenerateSerializer]
public class PipelineView : IAggregateView
{
    [Id(0)]
    public Guid OrganizationId { get; set; }

    [Id(1)]
    public Guid HubId { get; set; }


    [Id(2)]
    public Guid Id { get; set; } = Guid.NewGuid();


    [Id(3)]
    public string Name { get; set; } = PropertyDefaults.ERROR_NO_NAME;


    [Id(4)]
    public PipelineNodeView[] Nodes { get; set; } = Array.Empty<PipelineNodeView>();
}
