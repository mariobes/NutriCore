using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class EnumDisplayConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        if (string.IsNullOrWhiteSpace(value))
            throw new JsonException($"Empty value for enum {typeof(T).Name}");

        foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var display = field.GetCustomAttribute<DisplayAttribute>();
            if (display != null && display.Name == value)
            {
                return (T)field.GetValue(null)!;
            }
        }

        if (Enum.TryParse<T>(value, true, out var result))
            return result;

        throw new JsonException($"Invalid value '{value}' for enum {typeof(T).Name}");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        var field = typeof(T).GetField(value.ToString());
        var display = field?.GetCustomAttribute<DisplayAttribute>();

        writer.WriteStringValue(display?.Name ?? value.ToString());
    }
}