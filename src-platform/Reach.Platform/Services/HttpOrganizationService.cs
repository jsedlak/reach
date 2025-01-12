using Reach.Cqrs;
using Reach.Membership.ApiModel;
using Reach.Membership.Views;
using Reach.Platform.ServiceModel;
using System.Net.Http.Json;

namespace Reach.Platform.Services;

public class HttpOrganizationService : BaseGraphQlService, IOrganizationService
{
    //private static readonly MediaTypeHeaderValue ApplicationJsonMediaType = new MediaTypeHeaderValue("application/json");

    private readonly HttpClient _apiHttpClient;

    //private readonly JsonSerializerOptions _jsonOptions = new()
    //{
    //    PropertyNameCaseInsensitive = true
    //};

    public HttpOrganizationService(IHttpClientFactory httpClientFactory) 
        : base(httpClientFactory)
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
