namespace Reach.Platform.ServiceModel;

public interface IGraphQueryBuilder
{
    string GetDefaultEntityName<TModel>();
    
    string BuildBaseQuery<TModel>(string? entityName = null);
}