using System.Net.Http.Headers;

namespace Reach.Platform.Http;

public static class HttpConstants
{
    public static MediaTypeHeaderValue ApplicationJsonMediaType { get; } = new MediaTypeHeaderValue("application/json");
}