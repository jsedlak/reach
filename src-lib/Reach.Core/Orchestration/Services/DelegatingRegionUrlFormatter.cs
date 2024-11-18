using Reach.Orchestration.Model;
using Reach.Orchestration.ServiceModel;

namespace Reach.Orchestration.Services;

public class DelegatingRegionUrlFormatter : IRegionUrlFormatter
{
    private readonly Func<Region, string> _apiBaseUrlCallback;
    private readonly Func<Region, string> _graphBaseUrlCallback;

    public DelegatingRegionUrlFormatter(Func<Region, string> getApiBaseUrl, Func<Region, string> getGraphBaseUrl)
    {
        _apiBaseUrlCallback = getApiBaseUrl;
        _graphBaseUrlCallback = getGraphBaseUrl;
    }

    public string GetApiBaseUrl(Region region)
    {
        return _apiBaseUrlCallback(region);
    }

    public string GetGraphBaseUrl(Region region)
    {
        return _graphBaseUrlCallback(region);
    }
}
