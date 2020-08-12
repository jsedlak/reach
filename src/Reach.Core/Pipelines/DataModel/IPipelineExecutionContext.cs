using Reach.Diagnostics;

namespace Reach.Pipelines.DataModel
{
    public interface IPipelineExecutionContext
    {
        ILog Log { get; set; }

        PipelineExecutionOptions Options { get; set; }
    }
}
