using Reach.Cqrs;
using Reach.Membership.ApiModel;
using Reach.Membership.Views;
using Reach.Platform.ServiceModel;
using System.Net.Http.Json;

namespace Reach.Platform.Services;

internal class HttpOrganizationService : BaseGraphQlService, IOrganizationService
{
    private readonly HttpClient _apiHttpClient;

    public HttpOrganizationService(IHttpClientFactory httpClientFactory, IGraphQueryBuilder queryBuilder) 
        : base(httpClientFactory, queryBuilder)
    {
        _apiHttpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<IEnumerable<AvailableOrganizationView>> GetAvailableOrganizations()
    {
        return await GetMany<AvailableOrganizationView>("organizations");
    }

    public async Task<CommandResponse> Onboard(CreateOrganizationRequest request)
    {
        var response = await _apiHttpClient.PostAsJsonAsync("api/organizations", request);
        return await response.Content.ReadFromJsonAsync<CommandResponse>() ?? new();
    }
}
