using Reach.Pipelines.ServiceModel;
using SimpleInjector;
using System;

namespace Reach.Pipelines
{
    public interface ICanAddPipeline
    {
        ICanAddStep Register<TPipeline>();

        ICanAddStep Register(Type type);

        IPipelineExecutor Build(Container container);
    }
}
