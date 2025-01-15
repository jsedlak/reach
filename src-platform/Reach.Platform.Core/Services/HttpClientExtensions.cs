using Reach.Security;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Reach.Apps.ContentApp.Services;

public static class HttpClientExtensions 
{
    public static void SetAuthorization(this HttpClient client, ClaimsPrincipal principal)
    {
        var accessToken = principal.Claims.FirstOrDefault(m => m.Type == CustomClaimTypes.AccessToken)?.Value ?? "";

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }
}

