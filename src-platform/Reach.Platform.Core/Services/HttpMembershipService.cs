using Reach.Cqrs;
using Reach.Membership.Commands;
using Reach.Membership.Views;
using Reach.Platform.ServiceModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Reach.Platform.Services;

internal class HttpMembershipService : IMembershipService
{
    private readonly ICommandClient _commandClient;
    private readonly IGraphClient _graphClient;
    
    private readonly HttpClient _apiHttpClient;

    public HttpMembershipService(IHttpClientFactory httpClientFactory, ICommandClient commandClient, IGraphClient graphClient)
    {
        _commandClient = commandClient;
        _graphClient = graphClient;
        _apiHttpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<AccountSettingsView> GetAccountSettings()
    {
        return await _apiHttpClient.GetFromJsonAsync<AccountSettingsView>(
            "api/account/settings"
        ) ?? new();
    }

    public async Task<CommandResponse> SetSkipOnboardingFlag(SetSkipOnboardingFlagCommand command)
    {
        return await _commandClient.Execute("account", command);
    }
}
