using Microsoft.Extensions.Logging;
using Reach.Pipelines.Events;
using Reach.Pipelines.Views;
using Reach.Silo.Content.Grains;
using Reach.Silo.Pipelines.GrainModel;
using Reach.Silo.Pipelines.ServiceModel;

namespace Reach.Silo.Pipelines.Grains;

[ImplicitStreamSubscription(GrainConstants.Pipeline_EventStream)]
public class PipelineViewGrain : SubscribedViewGrain<BasePipelineEvent>,
    IPipelineViewGrain
{
    private readonly IPipelineViewReadRepository _pipelineViewReadRepository;
    private readonly IPipelineViewWriteRepository _pipelineViewWriteRepository;

    public PipelineViewGrain(
        IPipelineViewReadRepository pipelineViewReadRepository,
        IPipelineViewWriteRepository pipelineViewWriteRepository,
        ILogger<PipelineViewGrain> logger)
        : base(logger)
    {
        _pipelineViewReadRepository = pipelineViewReadRepository;
        _pipelineViewWriteRepository = pipelineViewWriteRepository;
    }

    public async Task Handle(PipelineCreatedEvent @event)
    {
        var result = await _pipelineViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        if(result is null)
        {
            result = new PipelineView
            {
                OrganizationId = @event.OrganizationId,
                HubId = @event.HubId,
                Id = @event.AggregateId,
                Name = @event.Name,
            };
        }

        result.Name = @event.Name;

        await _pipelineViewWriteRepository.Upsert(result);
    }

    public async Task Handle(NodeAddedToPipelineEvent @event)
    {
        var result = await _pipelineViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        result.Nodes = result.Nodes.Append(new PipelineNodeView
        {
            Id = @event.NodeId,
            Name = @event.Name,
        }).ToArray();

        await _pipelineViewWriteRepository.Upsert(result);
    }

    public async Task Handle(NodeRemovedFromPipelineEvent @event)
    {
        var result = await _pipelineViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        result.Nodes = result.Nodes
            .Where(x => x.Id != @event.NodeId)
            .ToArray();

        await _pipelineViewWriteRepository.Upsert(result);
    }

    public async Task Handle(VertexAddedToPipelineEvent @event)
    {
        var result = await _pipelineViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        var node = result.Nodes.First(m => m.Id == @event.SourceNodeId);
        node.Vertices = node.Vertices.Append(new PipelineVertexView
        {
            Id = @event.VertexId,
            DestinationNodeId = @event.DestinationNodeId,
        }).ToArray();

        await _pipelineViewWriteRepository.Upsert(result);
    }

    public async Task Handle(VertexRemovedFromPipelineEvent @event)
    {
        var result = await _pipelineViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        var node = result.Nodes.First(m => m.Id == @event.SourceNodeId);
        node.Vertices = node.Vertices
            .Where(x => x.Id != @event.VertexId)
            .ToArray();

        await _pipelineViewWriteRepository.Upsert(result);
    }

    public async Task Handle(PipelineDeletedEvent @event)
    {
        await _pipelineViewWriteRepository.Delete(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );
    }

    public async Task Handle(TransformerAddedToVertexEvent @event)
    {
        var result = await _pipelineViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        var node = result.Nodes.First(m => m.Id == @event.NodeId);
        var vertex = node.Vertices.First(m => m.Id == @event.VertexId);

        vertex.TransformerType = @event.TransformerType;
        vertex.TransformerParams = @event.TransformerParams;

        await _pipelineViewWriteRepository.Upsert(result);
    }

    public async Task Handle(TransformerRemovedFromVertexEvent @event)
    {
        var result = await _pipelineViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        var node = result.Nodes.First(m => m.Id == @event.NodeId);
        var vertex = node.Vertices.First(m => m.Id == @event.VertexId);

        vertex.TransformerType = null;
        vertex.TransformerParams = null;

        await _pipelineViewWriteRepository.Upsert(result);
    }

    public async Task Handle(PipelineRenamedEvent @event)
    {
        var result = await _pipelineViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        result.Name = @event.Name;

        await _pipelineViewWriteRepository.Upsert(result);
    }

    public async Task Handle(NodeTransformerSetEvent @event)
    {
        var result = await _pipelineViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        var node = result.Nodes.First(m => m.Id == @event.NodeId);
        node.TransformerType = @event.TransformerType;
        node.TransformerParams = @event.TransformerParams;

        await _pipelineViewWriteRepository.Upsert(result);
    }

    public async Task Handle(NodeRenamedEvent @event)
    {
        var result = await _pipelineViewReadRepository.Get(
            @event.OrganizationId,
            @event.HubId,
            @event.AggregateId
        );

        var node = result.Nodes.First(m => m.Id == @event.NodeId);
        node.Name = @event.Name;

        await _pipelineViewWriteRepository.Upsert(result);
    }
}