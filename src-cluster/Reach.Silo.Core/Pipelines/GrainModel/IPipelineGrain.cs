using Reach.Cqrs;
using Reach.Pipelines.Commands;

namespace Reach.Silo.Pipelines.GrainModel;

public interface IPipelineGrain : IGrainWithStringKey
{
    Task<CommandResponse> Create(CreatePipelineCommand command);

    Task<CommandResponse> AddNode(AddNodeToPipelineCommand command);

    Task<CommandResponse> RemoveNode(RemoveNodeFromPipelineCommand command);

    Task<CommandResponse> AddVertex(AddVertexToPipelineCommand command);

    Task<CommandResponse> AddTransformerToVertex(AddTransformerToVertexCommand command);

    Task<CommandResponse> Delete(DeletePipelineCommand command);

    Task<CommandResponse> RemoveTransformerFromVertex(RemoveTransformerFromVertexCommand command);

    Task<CommandResponse> Rename(RenamePipelineCommand command);

    Task<CommandResponse> SetNodeTransformer(SetNodeTransformerCommand command);

    Task<CommandResponse> RenameNode(RenameNodeCommand command);
}
