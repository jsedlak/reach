namespace Reach.Pipelines
{
    public static class MemoryPipelineExtensions
    {
        public static ICanAddPipeline UseMemoryPipelines(this PipelineExecutorBuilder builder)
        {
            builder.RegisterFactory(new MemoryPipelineExecutorFactory());
            return builder;
        }
    }
}
