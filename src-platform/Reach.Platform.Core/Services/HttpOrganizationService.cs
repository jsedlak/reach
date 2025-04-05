using Reach.Cqrs;
using Reach.Membership.ApiModel;
using Reach.Membership.Views;
using Reach.Platform.ServiceModel;
using System.Net.Http.Json;

namespace Reach.Platform.Services;

internal class HttpOrganizationService : IOrganizationService
{
    private readonly IGraphClient _graphClient;
    private readonly HttpClient _apiHttpClient;

    public HttpOrganizationService(IHttpClientFactory httpClientFactory, IGraphClient graphClient)
    {
        _graphClient = graphClient;
        _apiHttpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<IEnumerable<AvailableOrganizationView>> GetAvailableOrganizations()
    {
        return await _graphClient.GetMany<AvailableOrganizationView>();
    }

    public async Task<CommandResponse> Onboard(CreateOrganizationRequest request)
    {
        var response = await _apiHttpClient.PostAsJsonAsync("api/organizations", request);
        return await response.Content.ReadFromJsonAsync<CommandResponse>() ?? new();
    }

    public async Task<CommandResponse> CreateHub(CreateHubRequest request)
    {
        var response = await _apiHttpClient.PostAsJsonAsync($"api/organizations/{request.OrganizationId}/hubs", request);
        return await response.Content.ReadFromJsonAsync<CommandResponse>() ?? new();
    }

    public async Task<CommandResponse> ValidateOrganizationName(ValidateOrgNameRequest request)
    {
        var response = await _apiHttpClient.PostAsJsonAsync("api/organizations/validate", request);
        return await response.Content.ReadFromJsonAsync<CommandResponse>() ?? new();
    }

    public async Task<CommandResponse> ValidateHubName(ValidateHubNameRequest request)
    {
        var response = await _apiHttpClient.PostAsJsonAsync(
            $"api/organizations/{request.OrganizationId}/hubs/validate", 
            request
        );

        return await response.Content.ReadFromJsonAsync<CommandResponse>() ?? new();
    }
}
