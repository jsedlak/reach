namespace Reach.Security;

/// <summary>
/// Contains user information from the IDP that may be serialized
/// and sent across the Server/Client barrier
/// </summary>
public class SerializedUserInfo
{
    /// <summary>
    /// Gets or Sets the unique user identifier
    /// </summary>
    public string UserId { get; set; } = "";

    /// <summary>
    /// Gets or Sets the user's friendly name
    /// </summary>
    public string Name { get; set; } = "";


    /// <summary>
    /// Gets or Sets the users email
    /// </summary>
    public string Email { get; set; } = "";

    /// <summary>
    /// Gets or Sets the users access token
    /// </summary>
    public string AccessToken { get; set; } = "";

    /// <summary>
    /// Gets or Sets the users avatar URL
    /// </summary>
    public string AvatarUrl { get; set; } = "";
}
