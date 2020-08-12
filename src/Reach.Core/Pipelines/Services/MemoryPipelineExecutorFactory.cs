using Reach.Diagnostics;
using Reach.Pipelines.ServiceModel;
using SimpleInjector;
using System.Collections.Generic;

namespace Reach.Pipelines
{
    public class MemoryPipelineExecutorFactory : IPipelineExecutorFactory
    {
        public IPipelineExecutor CreateInstance(Container container, IEnumerable<object> pipelines)
        {
            return new MemoryPipelineExecutor(pipelines, container.GetInstance<ILog>());
        }
    }
}
