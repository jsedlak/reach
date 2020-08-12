using Reach.Diagnostics;
using Reach.Pipelines.DataModel;
using Reach.Pipelines.ServiceModel;
using Reach.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reach.Pipelines
{
    /// <summary>
    /// Memory based pipeline executor
    /// </summary>
    public class MemoryPipelineExecutor : IPipelineExecutor
    {
        private readonly IEnumerable<object> _pipelines;
        private readonly ILog _log;

        public MemoryPipelineExecutor(IEnumerable<object> pipelines, ILog log)
        {
            _pipelines = pipelines;
            _log = log;
        }

        public async Task<PipelineResult> ExecuteAsync(object request)
        {
            if(request == null)
            {
                return PipelineResult.Error(500, "Must execute pipeline with a request.");
            }

            var pipelineType = typeof(IPipeline<>).MakeGenericType(request.GetType());
            var matchingPipeline = _pipelines.FirstOrDefault(m => pipelineType.IsAssignableFrom(m.GetType()));

            if (matchingPipeline == null)
            {
                return PipelineResult.Error(404, "No pipeline can handle that request");
            }

            // create the instance
            var context = Activator.CreateInstance(
                typeof(PipelineExecutionContext<>).MakeGenericType(request.GetType())
            );

            // setup context values
            var abstractedContext = context as IPipelineExecutionContext;
            abstractedContext.Log = _log;

            // TODO: do we need to grab the specific overload?
            var method = matchingPipeline.GetType().GetMethod("ExecuteAsync");

            // fire off that beautiful method
            var task = (Task<PipelineResult>)method.Invoke(matchingPipeline, new[] { context });
            await task;

            return task.Result;
            
        }
    }
}
