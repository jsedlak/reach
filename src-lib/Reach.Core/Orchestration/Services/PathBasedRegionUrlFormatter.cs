using Reach.Orchestration.Model;
using Reach.Orchestration.ServiceModel;

namespace Reach.Orchestration.Services;

public class PathBasedRegionUrlFormatter : IRegionUrlFormatter
{
    private readonly string _format;
    private readonly string _regionFormat;
    private readonly string _apiRoute;
    private readonly string _graphRoute;

    public PathBasedRegionUrlFormatter(string format, string regionFormat, string apiRoute = "/v1/api",  string graphRoute = "/v1/graph")
    {
        if (format == null)
        {
            throw new ArgumentNullException(nameof(format));
        }

        if (regionFormat == null)
        {
            throw new ArgumentNullException(nameof(regionFormat));
        }

        _format = format.Trim().TrimEnd('/');
        _regionFormat = regionFormat.Trim().TrimEnd('/');
        _apiRoute = "/" + apiRoute.Trim().TrimStart('/');
        _graphRoute = "/" + graphRoute.Trim().TrimStart('/');
    }

    public string GetApiBaseUrl(Region region)
    {
        return $"{_regionFormat}{_apiRoute}".ToLower().Replace("{region}", region.Key.ToLower());
    }

    public string GetGraphBaseUrl(Region region)
    {
        return $"{_regionFormat}{_graphRoute}".ToLower().Replace("{region}", region.Key.ToLower());
    }

    public string GetOrganizationBaseUrl(Region region, string organizationSlug)
    {
        return $"{_format}/app/{organizationSlug}".ToLower().Replace("{region}", region.Key.ToLower());
    }

    public string GetHubBaseUrl(Region region, string organizationSlug, string hubSlug)
    {
        return $"{_format}/app/{organizationSlug}/{hubSlug}".ToLower().Replace("{region}", region.Key.ToLower());
    }
}
