using System.Net.Http.Headers;
using System.Text.Json;
using System.Windows.Input;
using Reach.Membership;
using Reach.Platform.Json;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Services;

internal sealed class DefaultGraphClient : IGraphClient
{
    private readonly IGraphQueryBuilder _queryBuilder;
    
    private readonly HttpClient _httpClient;

    public DefaultGraphClient(IHttpClientFactory httpClientFactory, IGraphQueryBuilder queryBuilder)
    {
        _queryBuilder = queryBuilder;
        _httpClient = httpClientFactory.CreateClient("graphql");
    }

    public async Task<IEnumerable<TModel>> GetMany<TModel>(
        Guid? organizationId = null,
        Guid? hubId = null,
        string? entityName = null, 
        string? query = null)
    {
        // build the query if we need to
        entityName ??= _queryBuilder.GetDefaultEntityName<TModel>();
        query ??= _queryBuilder.BuildBaseQuery<TModel>(entityName);

        // serialize the request query
        var requestContent = new StringContent(
            JsonSerializer.Serialize(new { query }),
            new MediaTypeHeaderValue("application/json")
        );

        // add our headers if we need them
        if (organizationId.HasValue)
        {
            requestContent.Headers.Add(MembershipHttpConstants.OrganizationIdHeader, organizationId.ToString());
        }

        if (hubId.HasValue)
        {
            requestContent.Headers.Add(MembershipHttpConstants.HubIdHeader, hubId.ToString());
        }

        // post the query to the endpoint
        var result = await _httpClient.PostAsync("graphql", requestContent);

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
            DefaultJsonOptions.Instance
        );

        return resultCollection as IEnumerable<TModel> ?? Array.Empty<TModel>();
    }
}