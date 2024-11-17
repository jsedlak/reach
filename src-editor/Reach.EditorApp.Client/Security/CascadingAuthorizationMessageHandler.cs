using Microsoft.AspNetCore.Components.Authorization;
using Reach.Security;

namespace Reach.EditorApp.Client.Security;

public class CascadingAuthorizationMessageHandler : DelegatingHandler
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public CascadingAuthorizationMessageHandler(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var accessToken = state.User.Claims.FirstOrDefault(m => m.Type.Equals(CustomClaimTypes.AccessToken))?.Value ?? "";

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}