using Reach.Extensions;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Services;

public abstract class BaseGraphQlService
{
    private readonly IGraphQueryBuilder _queryBuilder;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        // Converters = { new EditorParameterTypeConverter() },
        PropertyNameCaseInsensitive = true
    };

    protected BaseGraphQlService(IHttpClientFactory httpClientFactory, IGraphQueryBuilder queryBuilder)
    {
        _queryBuilder = queryBuilder;
        GraphQlClient = httpClientFactory.CreateClient("graphql");
    }

    protected async Task<IEnumerable<TModel>> GetMany<TModel>(string? entityName = null, string? query = null)
    {
        // build the query if we need to
        entityName ??= _queryBuilder.GetDefaultEntityName<TModel>();
        query ??= _queryBuilder.BuildBaseQuery<TModel>(entityName);

        // serialize the request query
        var requestContent = new StringContent(
            JsonSerializer.Serialize(new { query }),
            new MediaTypeHeaderValue("application/json")
        );

        // post the query to the endpoint
        var result = await GraphQlClient.PostAsync("graphql", requestContent);

        // get the response body and parse it into json
        var contentString = await result.Content.ReadAsStringAsync();
        using var jsonDocument = JsonDocument.Parse(contentString);

        // load contentStream as json document, reading editorDefinitions node and deserializing as IEnumerable<EditorDefinitionView>
        var jsonValue = jsonDocument.RootElement.GetProperty("data").GetProperty(entityName);

        using var jsonStream = new MemoryStream();
        using var jsonWriter = new Utf8JsonWriter(jsonStream);

        jsonValue.WriteTo(jsonWriter);
        await jsonWriter.FlushAsync();
        jsonStream.Position = 0;

        // read the json stream and parse it
        var resultCollection = await JsonSerializer.DeserializeAsync<IEnumerable<TModel>>(
            jsonStream,
            _jsonOptions
        );

        return resultCollection as IEnumerable<TModel> ?? Array.Empty<TModel>();
    }

    

    protected HttpClient GraphQlClient { get; private set; }
}
