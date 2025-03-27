using Reach.Pipelines.Events;

namespace Reach.Silo.Pipelines.GrainModel;

public interface IPipelineViewGrain : IGrainWithStringKey
{
    Task Handle(PipelineCreatedEvent @event);
    
    Task Handle(NodeAddedToPipelineEvent @event);
    
    Task Handle(NodeRemovedFromPipelineEvent @event);
    
    Task Handle(VertexAddedToPipelineEvent @event);
    
    Task Handle(VertexRemovedFromPipelineEvent @event);
    
    Task Handle(PipelineDeletedEvent @event);
    
    Task Handle(TransformerAddedToVertexEvent @event);
    
    Task Handle(TransformerRemovedFromVertexEvent @event);
    
    Task Handle(PipelineRenamedEvent @event);
    
    Task Handle(NodeTransformerSetEvent @event);
    
    Task Handle(NodeRenamedEvent @event);
}
