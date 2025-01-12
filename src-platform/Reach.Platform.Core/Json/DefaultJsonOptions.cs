using System.Text.Json;
using Reach.Platform.Json.Converters;

namespace Reach.Platform.Json;

public class DefaultJsonOptions
{
    public static JsonSerializerOptions Instance { get; private set; }

    static DefaultJsonOptions()
    {
        Instance = new()
        {
            Converters = { new EditorParameterTypeConverter() },
            PropertyNameCaseInsensitive = true
        };
    }
}