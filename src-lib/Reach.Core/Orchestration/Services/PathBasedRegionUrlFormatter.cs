using Reach.Orchestration.Model;
using Reach.Orchestration.ServiceModel;

namespace Reach.Orchestration.Services;

public class PathBasedRegionUrlFormatter : IRegionUrlFormatter
{
    private readonly string _format;
    private readonly string _apiRoute;
    private readonly string _graphRoute;

    public PathBasedRegionUrlFormatter(
        string format,
        string apiRoute = "/v1/api", 
        string graphRoute = "/v1/graph")
    {
        if (format == null)
        {
            throw new ArgumentNullException(nameof(format));
        }

        _format = format.Trim().TrimEnd('/');
        _apiRoute = "/" + apiRoute.Trim().TrimStart('/');
        _graphRoute = "/" + graphRoute.Trim().TrimStart('/');
    }

    public string GetApiBaseUrl(Region region)
    {
        return $"{_format}{_apiRoute}".ToLower().Replace("{region}", region.Key.ToLower());
    }

    public string GetGraphBaseUrl(Region region)
    {
        return $"{_format}{_graphRoute}".ToLower().Replace("{region}", region.Key.ToLower());
    }

    public string GetTenantBaseUrl(Region region, string tenantSlug = "")
    {
        return $"{_format}/app/{tenantSlug}".ToLower().Replace("{region}", region.Key.ToLower());
    }
}
