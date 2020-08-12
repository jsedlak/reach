using Reach.Pipelines.ServiceModel;

namespace Reach.Pipelines
{
    public interface ICanSetExecutorFactory
    {
        ICanAddPipeline RegisterFactory<TFactory>(TFactory factory) where TFactory : IPipelineExecutorFactory;
    }
}
