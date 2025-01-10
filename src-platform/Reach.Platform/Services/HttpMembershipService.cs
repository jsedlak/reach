using Reach.Membership.Views;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Services;

public class HttpMembershipService : BaseGraphQlService, IMembershipService
{
    private HttpClient _apiHttpClient;

    public HttpMembershipService(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory)
    {
        _apiHttpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<IEnumerable<AvailableOrganizationView>> GetAvailableOrganizations()
    {
        return await GetMany<AvailableOrganizationView>("organizations");
    }
}
