using Reach.Pipelines.DataModel;
using System.Threading.Tasks;

namespace Reach.Pipelines.ServiceModel
{
    public interface IPipelineStep<TRequest>
    {
        Task<PipelineResult> ExecuteAsync(PipelineExecutionContext<TRequest> context);
    }
}
