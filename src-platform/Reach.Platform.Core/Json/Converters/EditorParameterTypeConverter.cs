using System.Text.Json;
using System.Text.Json.Serialization;
using Reach.Content.Model;

namespace Reach.Platform.Json.Converters;

public sealed class EditorParameterTypeConverter : JsonConverter<EditorParameterType>
{
    public override EditorParameterType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        //if(reader.TryGetInt32(out var intValue))
        //{
        //    return (EditorParameterType)intValue;
        //}

        var val = reader.GetString();

        if(val is null)
        {
            return EditorParameterType.Text;
        }

        var enumName = Enum.GetNames(typeof(EditorParameterType)).FirstOrDefault(m => m.Equals(val, StringComparison.OrdinalIgnoreCase));

        if(enumName is null)
        {
            return EditorParameterType.Text;
        }

        return Enum.Parse<EditorParameterType>(enumName);
    }

    public override void Write(Utf8JsonWriter writer, EditorParameterType value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
