using Reach.Pipelines.DataModel;
using System.Threading.Tasks;

namespace Reach.Pipelines.ServiceModel
{
    public interface IPipelineExecutor
    {
        Task<PipelineResult> ExecuteAsync(object request);
    }
}
