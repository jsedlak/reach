using Microsoft.AspNetCore.Components.Authorization;
using Reach.Security;
using System.Security.Claims;

namespace Reach.EditorApp.Client.Authentication;

public static class AuthenticationStateExtensions
{
    public static UserContext AsContext(this AuthenticationState state)
    {
        var user = state.User;

        return new UserContext
        {
            Nickname = user.FindFirst(ClaimTypes.Name)?.Value ?? "",
            AvatarUrl = user.FindFirst(CustomClaimTypes.AvatarUrl)?.Value ?? ""
        };
    }
}
