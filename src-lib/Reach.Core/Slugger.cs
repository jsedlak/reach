using Slugify;
using System.Text.RegularExpressions;

namespace Reach;

public static class Slugger
{
    static Regex Pattern = new Regex(@"^\p{P}+|\p{P}+$");

    public static SlugHelper Instance { get; set; } = new();

    static Slugger()
    {
        SlugHelperConfiguration config = new SlugHelperConfiguration();

        // Replace spaces with a hyphens
        config.StringReplacements.Add(".", "-");
        config.StringReplacements.Add("_", "-");

        // We want a lowercase Slug
        config.ForceLowerCase = true;

        // Colapse consecutive whitespace chars into one
        config.CollapseWhiteSpace = true;
        config.CollapseDashes = true;

        // trim whitespace front and rear
        config.TrimWhitespace = true;

        // Remove everything that's not a letter, number, hyphen, dot, or underscore
        config.DeniedCharactersRegex = @"[^a-zA-Z0-9\-\._]";

        Instance = new SlugHelper(config);
    }

    public static string Slugify(this string input)
    {
        input = Pattern.Replace(input, "");
        return Instance.GenerateSlug(input);
    }
}
