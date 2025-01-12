namespace Reach.Platform.ServiceModel;

public interface IGraphClient
{
    Task<IEnumerable<TModel>> GetMany<TModel>(
        Guid? organizationId = null,
        Guid? hubId = null,
        string? entityName = null, 
        string? query = null
    );
}