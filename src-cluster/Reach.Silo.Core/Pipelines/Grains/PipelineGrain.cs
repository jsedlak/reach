using Reach.Cqrs;
using Reach.Pipelines.Commands;
using Reach.Pipelines.Events;
using Reach.Silo.Content.Grains;
using Reach.Silo.Pipelines.GrainModel;
using Reach.Silo.Pipelines.Model;

namespace Reach.Silo.Pipelines.Grains;

public class PipelineGrain : StreamingEventSourcedGrain<Pipeline, BasePipelineEvent>, 
    IPipelineGrain
{
    public PipelineGrain() 
        : base(GrainConstants.Pipeline_EventStream)
    {
    }

    public async Task<CommandResponse> AddNode(AddNodeToPipelineCommand command)
    {
        await Raise(new NodeAddedToPipelineEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            Name = command.Name
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> AddTransformerToVertex(AddTransformerToVertexCommand command)
    {
        await Raise(new TransformerAddedToVertexEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            TransformerId = Guid.NewGuid(),
            TransformerType = command.TransformerType,
            TransformerParams = command.TransformerParams
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> AddVertex(AddVertexToPipelineCommand command)
    {
        await Raise(new VertexAddedToPipelineEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            VertexId = Guid.NewGuid(),
            SourceNodeId = command.SourceNodeId,
            DestinationNodeId = command.DestinationNodeId
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> RemoveVertex(RemoveVertexFromPipelineCommand command)
    {
        await Raise(new VertexRemovedFromPipelineEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            SourceNodeId = command.SourceNodeId,
            VertexId = command.VertexId
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Create(CreatePipelineCommand command)
    {
        await Raise(new PipelineCreatedEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            Name = command.Name
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Delete(DeletePipelineCommand command)
    {
        await Raise(new PipelineDeletedEvent(command.OrganizationId, command.HubId, command.AggregateId));

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> RemoveNode(RemoveNodeFromPipelineCommand command)
    {
        await Raise(new NodeRemovedFromPipelineEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            NodeId = command.NodeId
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> RemoveTransformerFromVertex(RemoveTransformerFromVertexCommand command)
    {
        await Raise(new TransformerRemovedFromVertexEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            NodeId = command.NodeId,
            VertexId = command.VertexId,
            TransformerId = command.TransformerId
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Rename(RenamePipelineCommand command)
    {
        await Raise(new PipelineRenamedEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            Name = command.Name
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> RenameNode(RenameNodeCommand command)
    {
        await Raise(new NodeRenamedEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            NodeId = command.NodeId,
            Name = command.Name
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> SetNodeTransformer(SetNodeTransformerCommand command)
    {
        await Raise(new NodeTransformerSetEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            NodeId = command.NodeId,
            TransformerType = command.TransformerType,
            TransformerParams = command.TransformerParams
        });

        return CommandResponse.Success();
    }
}
