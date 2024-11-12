using Microsoft.Extensions.DependencyInjection;
using Reach.Content.Model;
using Reach.Content.Views;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Reach.Apps.ContentApp.Services;

public class EditorDefinitionService
{
    private static readonly MediaTypeHeaderValue ApplicationJsonMediaType = new MediaTypeHeaderValue("application/json");

    private const string DefaultQuery = @"
    query {
        editorDefinitions {
            id
            name
            editorType
            parameters {
              name
              displayName
              description
              type
            }
        }
    }
    ";

    private readonly HttpClient _graphQlClient;
    private readonly HttpClient _apiClient;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        Converters = { new EditorParameterTypeConverter() },
        PropertyNameCaseInsensitive = true
    };

    public EditorDefinitionService(IServiceProvider serviceProvider)
    {
        var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        _graphQlClient = factory.CreateClient("graphql");
        _apiClient = factory.CreateClient("api");
    }

    public async Task<IEnumerable<EditorDefinitionView>> GetEditorDefinitions(string? query = null)
    {
        query = JsonSerializer.Serialize(new { query = query ?? DefaultQuery });
        var result = await _graphQlClient.PostAsync(
            "graphql/", 
            new StringContent(query, mediaType: ApplicationJsonMediaType)
        );

        var contentString = await result.Content.ReadAsStringAsync();

        Console.WriteLine(contentString);
        //var contentStream = await result.Content.ReadAsStreamAsync();

        using var jsonDocument = JsonDocument.Parse(contentString);

        // load contentStream as json document, reading editorDefinitions node and deserializing as IEnumerable<EditorDefinitionView>
        // var jsonDocument = await JsonDocument.ParseAsync(contentString);
        var jsonValue = jsonDocument.RootElement.GetProperty("data").GetProperty("editorDefinitions");

        using var jsonStream = new MemoryStream();
        using var jsonWriter = new Utf8JsonWriter(jsonStream);

        jsonValue.WriteTo(jsonWriter);
        await jsonWriter.FlushAsync();
        jsonStream.Position = 0;

        // read from json stream into string and deserialize to IEnumerable<EditorDefinitionView>
        var editorDefinitions = await JsonSerializer.DeserializeAsync<IEnumerable<EditorDefinitionView>>(
            jsonStream,
            _jsonOptions
        );
            
        return editorDefinitions as IEnumerable<EditorDefinitionView> ?? Array.Empty<EditorDefinitionView>();
    }
}

public sealed class EditorParameterTypeConverter : JsonConverter<EditorParameterType>
{
    public override EditorParameterType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //if(reader.TryGetInt32(out var intValue))
        //{
        //    return (EditorParameterType)intValue;
        //}

        var val = reader.GetString();

        if(val is null)
        {
            return EditorParameterType.Text;
        }

        var enumName = Enum.GetNames(typeof(EditorParameterType)).FirstOrDefault(m => m.Equals(val, StringComparison.OrdinalIgnoreCase));

        if(enumName is null)
        {
            return EditorParameterType.Text;
        }

        return Enum.Parse<EditorParameterType>(enumName);
    }

    public override void Write(Utf8JsonWriter writer, EditorParameterType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}