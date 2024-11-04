namespace Reach.Security;

/// <summary>
/// Provides a set of claim types for use with the serialized user information
/// </summary>
public static class CustomClaimTypes
{
    /// <summary>
    /// Provides access to the user's unique identifier
    /// </summary>
    public static readonly string Identifier = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

    /// <summary>
    /// Provides access to the friendly name of the user
    /// </summary>
    public static readonly string Nickname = "nickname";

    /// <summary>
    /// Provides access to the user's email address
    /// </summary>
    public static readonly string Email = "name";

    /// <summary>
    /// Represents access to the access_token for passing along to API calls
    /// </summary>
    public static readonly string AccessToken = "access_token";

    /// <summary>
    /// Represents the user's avatar URL
    /// </summary>
    public static readonly string AvatarUrl = "picture";
}
