using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;

namespace Reach.Extensions;

public static class StringExtensions
{
    public static Guid ToGuid(this string input)
    {
        using var md5 = MD5.Create();

        byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
        return new Guid(hash);
    }

    public static string TrimStart(this string input, string trim)
    {
        if (!input.StartsWith(trim, StringComparison.OrdinalIgnoreCase))
        {
            return input;
        }

        return input.Substring(trim.Length);
    }

    public static string ToCamelCase(this string s)
    {
        var x = s.Replace("_", "");

        if (x.Length == 0)
        {
            return "null";
        }

        x = Regex.Replace(x, "([A-Z])([A-Z]+)($|[A-Z])",
            m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);
        return char.ToLower(x[0]) + x.Substring(1);
    }

    public static string ToPascalCase(this string s)
    {
        var x = s.ToLower(); // ToCamelCase(s);
        x = Regex.Replace(x, "(^|\\s|_|-)([a-z])", m => m.Value.ToUpper());
        return x;
        // return // char.ToUpper(x[0]) + x.Substring(1);
    }
}