using Microsoft.AspNetCore.Components.Authorization;
using Reach.Security;

namespace Reach.EditorApp.Client.Authentication;

public static class AuthenticationStateExtensions
{
    public static UserContext AsContext(this AuthenticationState state)
    {
        var user = state.User;

        return new UserContext(
            user.FindFirst(CustomClaimTypes.Identifier)?.Value ?? "", 
            user.FindFirst(CustomClaimTypes.Nickname)?.Value ?? "",
            user.FindFirst(CustomClaimTypes.AvatarUrl)?.Value ?? ""
        );
    }
}
