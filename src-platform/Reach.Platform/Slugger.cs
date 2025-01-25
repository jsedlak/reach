namespace Reach.Platform;

public static class Slugger
{
    public static Slugify.SlugHelper Instance { get; set; } = new();

    public static string Slugify(this string input)
    {
        return Instance.GenerateSlug(input);
    }
}
