namespace Reach.ServiceModel
{
    public static class ServiceResultExtensions
    {
        public static TModel ReadAs<TModel>(this ServiceResult pipelineResult)
        {
            return (pipelineResult != null && pipelineResult.Data != null && pipelineResult.Data is TModel) ?
                (TModel)pipelineResult.Data : default(TModel);
        }
    }
}
