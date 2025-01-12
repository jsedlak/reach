using Reach.Cqrs;
using Reach.Membership.Commands;
using Reach.Membership.Views;
using Reach.Platform.ServiceModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Reach.Platform.Services;

public class HttpMembershipService : BaseGraphQlService, IMembershipService
{
    private static readonly MediaTypeHeaderValue ApplicationJsonMediaType = new MediaTypeHeaderValue("application/json");

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _apiHttpClient;

    public HttpMembershipService(IHttpClientFactory httpClientFactory)
        : base(httpClientFactory)
    {
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
        return await ExecuteCommandAsync(command);
    }

    private async Task<CommandResponse> ExecuteCommandAsync<TCommand>(TCommand command)
        where TCommand : BaseAccountCommand
    {
        // create and secure the request
        var content = new StringContent(JsonSerializer.Serialize(command, _jsonOptions), mediaType: ApplicationJsonMediaType);

        content.Headers.Add("X-Command-Type", typeof(TCommand).AssemblyQualifiedName);

        var response = await _apiHttpClient.PostAsync(
            $"api/account/execute",
            content
        );

        return await response.Content.ReadFromJsonAsync<CommandResponse>(_jsonOptions) ??
            new CommandResponse();
    }
}
