using Reach.Pipelines.Events;

namespace Reach.Silo.Pipelines.Model;

public class Pipeline
{
    public void Apply(PipelineCreatedEvent @event)
    {
        OrganizationId = @event.OrganizationId;
        HubId = @event.HubId;
        Id = @event.AggregateId;
        Name = @event.Name;
    }

    public void Apply(PipelineDeletedEvent @event)
    {
        IsDeleted = true;
    }

    public void Apply(PipelineRenamedEvent @event)
    {
        Name = @event.Name;
    }

    #region Nodes
    public void Apply(NodeAddedToPipelineEvent @event)
    {
        var newNode = new PipelineNode
        {
            Id = @event.NodeId,
            Name = @event.Name,
            IsEntry = @event.IsEntry
        };

        Nodes = Nodes.Append(newNode).ToArray();
    }

    public void Apply(NodeRemovedFromPipelineEvent @event)
    {
        Nodes = Nodes.Where(m => m.Id != @event.NodeId).ToArray();
    }

    public void Apply(NodeRenamedEvent @event)
    {
        var node = Nodes.First(m => m.Id == @event.NodeId);
        node.Name = @event.Name;
    }

    public void Apply(NodeTransformerSetEvent @event)
    {
        var node = Nodes.First(m => m.Id == @event.NodeId);
        node.Transformer = new TransformerDescription()
        {
            TransformerType = @event.TransformerType,
            TransformerParams = @event.TransformerParams
        };
    }
    #endregion

    #region Vertices
    public void Apply(VertexAddedToPipelineEvent @event)
    {
        var node = Nodes.First(m => m.Id == @event.SourceNodeId);
        node.Vertices = node.Vertices.Append(new PipelineVertex
        {
            Id = @event.VertexId,
            DestinationNodeId = @event.DestinationNodeId
        }).ToArray();
    }

    public void Apply(VertexRemovedFromPipelineEvent @event)
    {
        var node = Nodes.First(m => m.Id == @event.SourceNodeId);
        node.Vertices = node.Vertices.Where(m => m.Id != @event.VertexId).ToArray();
    }

    public void Apply(TransformerAddedToVertexEvent @event)
    {
        var node = Nodes.First(m => m.Id == @event.NodeId);
        var vertex = node.Vertices.First(m => m.Id == @event.VertexId);
        vertex.Transformers = vertex.Transformers.Append(new TransformerDescription
        {
            TransformerType = @event.TransformerType,
            TransformerParams = @event.TransformerParams
        }).ToArray();
    }

    public void Apply(TransformerRemovedFromVertexEvent @event)
    {
        var node = Nodes.First(m => m.Id == @event.NodeId);
        var vertex = node.Vertices.First(m => m.Id == @event.VertexId);
        vertex.Transformers = vertex.Transformers
            .Where(m => m.Id != @event.TransformerId).ToArray();
    }
    #endregion

    public Guid OrganizationId { get; set; }

    public Guid HubId { get; set; }

    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public PipelineNode[] Nodes { get; set; } = Array.Empty<PipelineNode>();

    public bool IsDeleted { get; set; }
}
