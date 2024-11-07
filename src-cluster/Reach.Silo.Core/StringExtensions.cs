using Slugify;

namespace Reach.Silo;

public static class StringExtensions
{
    public static SlugHelper HelperInstance { get; } = new();

    public static string ToSlug(this string value)
    {
        return HelperInstance.GenerateSlug(value);
    }
}
