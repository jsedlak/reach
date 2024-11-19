using Reach.Orchestration.Model;
using System.Net.Http.Json;

namespace Reach.EditorApp.Client.Services;

public class HttpRegionService
{
    private HttpClient _httpClient;

    public HttpRegionService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("global");
    }

    public async Task<IEnumerable<Region>> GetAllRegionsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Region>>("/api/regions") ?? [];
    }
}
