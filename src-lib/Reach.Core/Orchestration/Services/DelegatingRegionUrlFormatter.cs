using Reach.Orchestration.Model;
using Reach.Orchestration.ServiceModel;

namespace Reach.Orchestration.Services;

public class DelegatingRegionUrlFormatter : IRegionUrlFormatter
{
    private readonly Func<Region, string> _apiBaseUrlCallback;
    private readonly Func<Region, string> _graphBaseUrlCallback;
    private readonly Func<Region, string> _tenantBaseUrlCallback;

    public DelegatingRegionUrlFormatter(Func<Region, string> getApiBaseUrl, 
        Func<Region, string> getGraphBaseUrl,
        Func<Region, string> getTenantBaseUrl)
    {
        _apiBaseUrlCallback = getApiBaseUrl;
        _graphBaseUrlCallback = getGraphBaseUrl;
        _tenantBaseUrlCallback = getTenantBaseUrl;
    }

    public string GetApiBaseUrl(Region region)
    {
        return _apiBaseUrlCallback(region);
    }

    public string GetGraphBaseUrl(Region region)
    {
        return _graphBaseUrlCallback(region);
    }

    public string GetTenantBaseUrl(Region region)
    {
        return _tenantBaseUrlCallback(region);
    }
}
