using Reach.Extensions;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Reach.Platform.Services;

public abstract class BaseGraphQlService
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        // Converters = { new EditorParameterTypeConverter() },
        PropertyNameCaseInsensitive = true
    };

    protected BaseGraphQlService(IHttpClientFactory httpClientFactory)
    {
        GraphQlClient = httpClientFactory.CreateClient("graphql");
    }

    protected async Task<IEnumerable<TModel>> GetMany<TModel>(string entityName, string query = null)
    {
        if(query is null)
        {
            query = GenerateBaseQuery<TModel>(entityName);
        }

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

    private MediaTypeHeaderValue? MediaTypeHeaderValue(string v)
    {
        throw new NotImplementedException();
    }

    private string GenerateBaseQuery<TModel>(string entityName)
    {
        return "query { \r\n" + GenerateBaseQuery(entityName, typeof(TModel)) + "\r\n}";
    }

    private string GenerateBaseQuery(string entityName, Type entityType)
    {
        Console.WriteLine($"Registering {entityName} -> {entityType.Name}");
        var sb = new StringBuilder();
        sb.AppendLine($"{entityName}{{");

        var props = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in props)
        {
            if (prop.PropertyType.IsClass && prop.PropertyType != typeof(string))
            {
                var propType = prop.PropertyType;

                if(propType.IsArray)
                {
                    propType = propType.GetElementType();
                }
                else if(propType.IsEnumerableType())
                {
                    propType = propType.GetEnumerableType();
                }

                sb.AppendLine(
                    GenerateBaseQuery(prop.Name.ToCamelCase(), propType)
                );
            }
            else
            {
                sb.AppendLine($"{prop.Name.ToCamelCase()}");
            }
        }

        sb.AppendLine("}");

        return sb.ToString();
    }

    protected HttpClient GraphQlClient { get; private set; }
}
