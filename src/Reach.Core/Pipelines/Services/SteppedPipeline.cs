using Reach.Pipelines.DataModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reach.Pipelines.ServiceModel;
using Newtonsoft.Json;

namespace Reach.Pipelines.Services
{
    public abstract class SteppedPipeline<TRequest> : IPipeline<TRequest>
    {
        private IEnumerable<object> _steps;

        public SteppedPipeline(IEnumerable<object> steps)
        {
            _steps = steps;
        }

        public async Task<PipelineResult> ExecuteAsync(PipelineExecutionContext<TRequest> context)
        {
            var result = new PipelineResult();

            if(result == null)
            {
                return PipelineResult.Error(500, "Cannot build initial result object");
            }

            foreach(var step in _steps.Where(m=>m is IPipelineStep<TRequest>).Select(m => m as IPipelineStep<TRequest>))
            {
                // execute!!
                var stepResult = await step.ExecuteAsync(context);

                // calculate the running result aggregation
                result.IsSuccessful &= stepResult.IsSuccessful;
                result.Messages = result.Messages == null ? stepResult.Messages : result.Messages.Union(stepResult.Messages);
                result.Events = result.Events == null ? stepResult.Events : result.Events.Union(stepResult.Events);

                // do we need to get out of here because of an error?
                if(context.Options.BreakOnError && !result.IsSuccessful)
                {
                    return result;
                }
            }

            return result;
        }
    }
}
