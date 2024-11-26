using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reach.Components.Context;
using Reach.Cqrs;
using Reach.EditorApp.ServiceModel;
using Reach.Membership;
using Reach.Membership.Views;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Reach.Apps.ContentApp.Services;

public abstract class BaseService
{
    private static readonly MediaTypeHeaderValue ApplicationJsonMediaType = new MediaTypeHeaderValue("application/json");

    private readonly HttpClient _graphQlClient;
    private readonly HttpClient _apiClient;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        Converters = { new EditorParameterTypeConverter() },
        PropertyNameCaseInsensitive = true
    };

    private readonly NavigationManager _navigationManager;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILogger _logger;
    private readonly ITenantService _tenantService;

    protected BaseService(ITenantService tenantService, NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider, IHttpClientFactory httpClientFactory, ILogger logger)
    {
        _navigationManager = navigationManager;
        _authenticationStateProvider = authenticationStateProvider;
        _logger = logger;
        _tenantService = tenantService;

        _graphQlClient = httpClientFactory.CreateClient("graphql");
        _apiClient = httpClientFactory.CreateClient("api");
    }

    /// <summary>
    /// Ensures the tenant can be loaed from the URL and is valid for 
    /// the current user. If not, it redirects the user to the 
    /// dashboard and returns false.
    /// </summary>
    /// <returns></returns>
    private async Task<AvailableTenantView?> EnsureTenant()
    {
        var tenants = await _tenantService.GetTenantsForUserAsync();
        var path = _navigationManager.ToBaseRelativePath(_navigationManager.Uri).ToLower();

        var pathSplit = path.Split(["/"], StringSplitOptions.RemoveEmptyEntries);

        if (pathSplit.Length > 1 && pathSplit[0].Equals("app"))
        {
            return tenants.FirstOrDefault(m => m.Slug == pathSplit[1]);
        }

        return null;
    }

    /// <summary>
    /// Prepares an HTTP client with the custom headers we'll need
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    private async Task<AvailableTenantView> PrepareClient(HttpClient client, Expression<Func<AvailableTenantView, string>> getUrl)
    {
        var tenant = await EnsureTenant();

        if (tenant == null)
        {
            throw new UnauthorizedAccessException();
        }

        client.BaseAddress = new Uri(getUrl.Compile().Invoke(tenant));

        // Remove the tenant id header if it's been set already
        if (client.DefaultRequestHeaders.Contains(TenantHttpConstants.TenantIdHeader))
        {
            client.DefaultRequestHeaders.Remove(TenantHttpConstants.TenantIdHeader);
        }

        // Add the tenant id to ensure it's up to date
        client.DefaultRequestHeaders.Add(
            TenantHttpConstants.TenantIdHeader,
            tenant.TenantId.ToString()
        );

        return tenant;
    }

    protected async Task<CommandResponse> ExecuteCommandAsync<TCommand>(string path, TCommand command)
        where TCommand : AggregateCommand
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();

        if (state == null)
        {
            throw new UnauthorizedAccessException();
        }

        // apply security
        _logger.LogInformation("Preparing API Client");
        var tenant = await PrepareClient(_apiClient, m => m.Region.ApiUrl);
        _apiClient.SetAuthorization(state.User);

        // create and secure the request
        var content = new StringContent(JsonSerializer.Serialize(command, _jsonOptions), mediaType: ApplicationJsonMediaType);

        content.Headers.Add("X-Command-Type", typeof(TCommand).AssemblyQualifiedName);
        content.Headers.Add(TenantHttpConstants.TenantIdHeader, tenant.TenantId.ToString());

        var response = await _apiClient.PostAsync(
            $"api/{path}/{command.AggregateId}/execute",
            content
        );

        return await response.Content.ReadFromJsonAsync<CommandResponse>(_jsonOptions) ??
            new CommandResponse();
    }

    protected async Task<IEnumerable<TView>> QueryGraphAsync<TView>(string prop, string query)
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
       
        if(state == null)
        {
            throw new InvalidOperationException("You must be logged in to execute a graphql request.");
        }

        // apply security
        var tenant = await PrepareClient(_apiClient, m => m.Region.GraphUrl);
        _graphQlClient.SetAuthorization(state.User);

        // create and secure the request
        query = JsonSerializer.Serialize(new { query });
        var requestContent = new StringContent(query, mediaType: ApplicationJsonMediaType);

        // add our headers
        requestContent.Headers.Add(TenantHttpConstants.TenantIdHeader, tenant.TenantId.ToString());

        // bundle up the graphql request
        var result = await _graphQlClient.PostAsync("graphql/", requestContent);

        // get the response body and parse it into json
        var contentString = await result.Content.ReadAsStringAsync();
        using var jsonDocument = JsonDocument.Parse(contentString);

        // load contentStream as json document, reading editorDefinitions node and deserializing as IEnumerable<EditorDefinitionView>
        var jsonValue = jsonDocument.RootElement.GetProperty("data").GetProperty(prop);

        using var jsonStream = new MemoryStream();
        using var jsonWriter = new Utf8JsonWriter(jsonStream);

        jsonValue.WriteTo(jsonWriter);
        await jsonWriter.FlushAsync();
        jsonStream.Position = 0;

        // read the json stream and parse it
        var resultCollection = await JsonSerializer.DeserializeAsync<IEnumerable<TView>>(
            jsonStream,
            _jsonOptions
        );

        return resultCollection as IEnumerable<TView> ?? Array.Empty<TView>();
    }

    protected HttpClient ApiClient => _apiClient;

    protected HttpClient GraphQlClient => _graphQlClient;
}

