using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reach.Pipelines.ServiceModel
{
    public interface IPipelineExecutorFactory
    {
        IPipelineExecutor CreateInstance(Container container, IEnumerable<object> pipelines);
    }
}
