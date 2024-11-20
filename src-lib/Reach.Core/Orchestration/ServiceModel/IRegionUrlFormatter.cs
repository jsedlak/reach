using Microsoft.Extensions.Hosting;
using Reach.Orchestration.Model;

namespace Reach.Orchestration.ServiceModel;

public interface IRegionUrlFormatter
{
    string GetApiBaseUrl(Region region);

    string GetGraphBaseUrl(Region region);

    string GetTenantBaseUrl(Region region, string tenantSlug = "");
}
