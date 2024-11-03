namespace Reach.Security;

/// <summary>
/// Provides a set of claim types for use with the serialized user information
/// </summary>
public static class CustomClaimTypes
{
    /// <summary>
    /// Represents access to the access_token for passing along to API calls
    /// </summary>
    public static readonly string AccessToken = "access_token";
}
