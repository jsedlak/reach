using Microsoft.Extensions.DependencyInjection;
using Reach.Content.Commands.Editors;
using Reach.Cqrs;
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

    protected BaseService(IServiceProvider serviceProvider)
    {
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        _graphQlClient = factory.CreateClient("graphql");
        _apiClient = factory.CreateClient("api");
    }

    protected async Task<CommandResponse> ExecuteCommandAsync<TCommand>(string path, TCommand command)
        where TCommand : AggregateCommand
    {
        var content = new StringContent(JsonSerializer.Serialize(command, _jsonOptions), mediaType: ApplicationJsonMediaType);
        content.Headers.Add("X-Command-Type", typeof(TCommand).AssemblyQualifiedName);

        var response = await _apiClient.PostAsync(
            $"api/{path}/{command.AggregateId}/execute",
            content
        );

        return await response.Content.ReadFromJsonAsync<CommandResponse>(_jsonOptions) ??
            new CommandResponse();
    }

    protected async Task<IEnumerable<TView>> QueryGraphAsync<TView>(string prop, string query)
    {
        // bundle up the graphql request
        query = JsonSerializer.Serialize(new { query = query });
        var result = await _graphQlClient.PostAsync(
            "graphql/",
            new StringContent(query, mediaType: ApplicationJsonMediaType)
        );

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
