using Reach.Cqrs;
using Reach.Membership.ApiModel;
using Reach.Membership.Views;
using Reach.Platform.ServiceModel;
using System.Net.Http.Json;

namespace Reach.Platform.Services;

internal class HttpOrganizationService : IOrganizationService
{
    private readonly ICommandClient _commandClient;
    private readonly IGraphClient _graphClient;
    private readonly HttpClient _apiHttpClient;

    public HttpOrganizationService(IHttpClientFactory httpClientFactory, ICommandClient commandClient, IGraphClient graphClient)
    {
        _commandClient = commandClient;
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
}
