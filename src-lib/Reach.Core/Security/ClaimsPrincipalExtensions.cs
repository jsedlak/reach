using System.Security.Claims;

namespace Reach.Security;

public static class ClaimsPrincipalExtensions
{
    public static string GetIdentifierClaim(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.Claims.FirstOrDefault(m => m.Type == CustomClaimTypes.Identifier)?.Value ?? "";
    }
}
