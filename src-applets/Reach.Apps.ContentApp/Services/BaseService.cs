using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Reach.Cqrs;
using Reach.Membership;
using Reach.Membership.ServiceModel;
using Reach.Membership.Views;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Reach.Apps.ContentApp.Services;

public abstract class BaseService
{
    private static readonly MediaTypeHeaderValue ApplicationJsonMediaType = new MediaTypeHeaderValue("application/json");

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        Converters = { new EditorParameterTypeConverter() },
        PropertyNameCaseInsensitive = true
    };

    private readonly NavigationManager _navigationManager;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILogger _logger;
    private readonly IOrganizationService _organizationService;

    protected BaseService(IOrganizationService organizationService, NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider, IHttpClientFactory httpClientFactory, ILogger logger)
    {
        _navigationManager = navigationManager;
        _authenticationStateProvider = authenticationStateProvider;
        _logger = logger;
        _organizationService = organizationService;
        _httpClientFactory = httpClientFactory;

        //_graphQlClient = httpClientFactory.CreateClient("graphql");
        //_apiClient = httpClientFactory.CreateClient("api");
    }

    /// <summary>
    /// Ensures the tenant can be loaded from the URL and is valid for 
    /// the current user. If not, it redirects the user to the 
    /// dashboard and returns false.
    /// </summary>
    /// <returns></returns>
    private async Task<MembershipView> EnsureMembership()
    {
        var view = new MembershipView();

        var organizations = await _organizationService.GetOrganizationsForUserAsync();
        var path = _navigationManager.ToBaseRelativePath(_navigationManager.Uri).ToLower();

        var pathSplit = path.Split(["/"], StringSplitOptions.RemoveEmptyEntries);

        // TODO: We need to get this from a Region formatter, don't we?
        if (pathSplit.Length > 2 && pathSplit[0].Equals("app"))
        {
            view.Organization = organizations.FirstOrDefault(m => m.Slug == pathSplit[1]);
            view.Hub = view.Organization?.Hubs.FirstOrDefault(m => m.Slug == pathSplit[2]);
        }

        return view;
    }

    /// <summary>
    /// Prepares an HTTP client with the custom headers we'll need
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    private async Task<MembershipView> PrepareClient(HttpClient client, Expression<Func<MembershipView, string>> getUrl)
    {
        var membership = await EnsureMembership();

        if (membership == null || !membership.IsValid)
        {
            throw new UnauthorizedAccessException();
        }

        client.BaseAddress = new Uri(getUrl.Compile().Invoke(membership));

        return membership;
    }

    private void AddHeaders(HttpContent content, MembershipView membership)
    {
        content.Headers.Add(MembershipHttpConstants.OrganizationIdHeader, membership.Organization!.Id.ToString());
        content.Headers.Add(MembershipHttpConstants.HubIdHeader, membership.Hub!.Id.ToString());
    }

    protected async Task<CommandResponse> ExecuteCommandAsync<TCommand>(string path, TCommand command)
        where TCommand : AggregateCommand
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();

        if (state == null)
        {
            throw new UnauthorizedAccessException();
        }

        var client = _httpClientFactory.CreateClient("api");

        // apply security
        _logger.LogInformation("Preparing API Client");
        var membership = await PrepareClient(client, m => m.Hub!.Region.ApiUrl);
        client.SetAuthorization(state.User);

        // create and secure the request
        var content = new StringContent(JsonSerializer.Serialize(command, _jsonOptions), mediaType: ApplicationJsonMediaType);

        content.Headers.Add("X-Command-Type", typeof(TCommand).AssemblyQualifiedName);
        AddHeaders(content, membership);

        var response = await client.PostAsync(
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

        var client = _httpClientFactory.CreateClient("graphql");

        // apply security
        var membership = await PrepareClient(client, m => m.Hub!.Region.GraphUrl);
        client.SetAuthorization(state.User);

        // create and secure the request
        query = JsonSerializer.Serialize(new { query });
        var requestContent = new StringContent(query, mediaType: ApplicationJsonMediaType);

        // add our headers
        AddHeaders(requestContent, membership);

        // bundle up the graphql request
        var result = await client.PostAsync("graphql/", requestContent);

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

    private class MembershipView
    {
        public AvailableOrganizationView? Organization { get; set; }

        public AvailableHubView? Hub { get; set; }

        public bool IsValid => Organization != null && Hub != null;
    }
}

