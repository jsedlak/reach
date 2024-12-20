using Microsoft.Extensions.Hosting;
using Reach.Orchestration.Model;

namespace Reach.Orchestration.ServiceModel;

public interface IRegionUrlFormatter
{
    string GetApiBaseUrl(Region region);

    string GetGraphBaseUrl(Region region);

    string GetOrganizationBaseUrl(Region region, string organizationSlug);

    string GetHubBaseUrl(Region region, string organizationSlug, string hubSlug);
}
