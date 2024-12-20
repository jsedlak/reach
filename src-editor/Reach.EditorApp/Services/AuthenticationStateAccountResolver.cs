using Microsoft.AspNetCore.Components.Authorization;
using Reach.Membership.ServiceModel;

namespace Reach.EditorApp.Services;

public class AuthenticationStateAccountResolver : IAccountResolver
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationStateAccountResolver(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<string> GetCurrentAccountAsync()
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();

        if (state is null || state.User.Identity is null || state.User.Identity.Name is null)
        {
            return string.Empty;
        }

        return state.User.Identity.Name;
    }
}
