namespace Reach.EditorApp.Client.Authentication;

public sealed class UserContext
{
    public string UserId { get; set; } = string.Empty;

    public string Nickname { get; init; }

    public string AvatarUrl { get; init; }
}