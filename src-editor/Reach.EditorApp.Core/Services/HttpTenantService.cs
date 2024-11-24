using Reach.Cqrs;
using Reach.EditorApp.ServiceModel;
using Reach.Membership.Model;
using Reach.Membership.Views;
using System.Net.Http.Json;

namespace Reach.EditorApp.Services;

public class HttpTenantService : ITenantService
{
    private HttpClient _httpClient;

    public HttpTenantService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("global");
    }

    public async Task<IEnumerable<AvailableTenantView>> GetTenantsForUserAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<AvailableTenantView>>("/api/tenants") ?? [];
    }

    public async Task<CommandResponse> CreateAsync(Tenant tenant)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/tenants", tenant);
        var result = await response.Content.ReadFromJsonAsync<CommandResponse>();
        return result ?? new();
    }
}