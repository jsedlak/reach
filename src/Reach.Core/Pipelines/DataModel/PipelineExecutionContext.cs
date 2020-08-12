using Reach.Diagnostics;

namespace Reach.Pipelines.DataModel
{
    public class PipelineExecutionContext<TRequest> : IPipelineExecutionContext
    {
        public ILog Log { get; set; }

        public TRequest Request { get; set; }

        public PipelineExecutionOptions Options { get; set; } = PipelineExecutionOptions.Default;
    }
}
