using Reach.Cqrs;
using Reach.EditorApp.ApiModel;
using Reach.Membership.ServiceModel;
using Reach.Membership.Views;
using System.Net.Http.Json;

namespace Reach.EditorApp.Services;

public class HttpOrganizationService : IOrganizationService
{
    private HttpClient _httpClient;

    public HttpOrganizationService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("global");
    }

    public async Task<CommandResponse> CreateHub(Guid id, Guid organizationId, string name, string slug, string iconUrl, string regionKey)
    {
        var response = await _httpClient.PostAsJsonAsync($"/api/organizations/{organizationId}/hubs", new CreateHubRequest
        {
            Id = id,
            Name = name,
            Slug = slug,
            OrganizationId = organizationId,
            IconUrl = iconUrl,
            RegionKey = regionKey
        });

        var result = await response.Content.ReadFromJsonAsync<CommandResponse>();
        return result ?? new();
    }

    public async Task<CommandResponse> CreateOrganization(Guid id, string name, string slug, string ownerId)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/organizations", new CreateOrganizationRequest
        {
            Id = id,
            Name = name,
            Slug = slug
        });

        var result = await response.Content.ReadFromJsonAsync<CommandResponse>();
        return result ?? new();
    }

    public async Task<IEnumerable<AvailableOrganizationView>> GetOrganizationsForUserAsync(string userId)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<AvailableOrganizationView>>("/api/organizations") ?? [];
    }
}