using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Reach.EditorApp.Client.Components.Providers;

public partial class AuthDebug : ComponentBase
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private ClaimsPrincipal? _user;

    public AuthDebug(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }
    
    protected override async Task OnInitializedAsync()
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        
        _user = state.User;
    }
}