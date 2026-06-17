using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EternalQuest
{
    public class GoalConverter : JsonConverter<Goal>
    {
        public override Goal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                if (!root.TryGetProperty("_type", out JsonElement typeElement))
                {
                    throw new JsonException("Missing _type property");
                }

                string type = typeElement.GetString();
                string json = root.GetRawText();

                return type switch
                {
                    "Simple" => JsonSerializer.Deserialize<SimpleGoal>(json, options),
                    "Eternal" => JsonSerializer.Deserialize<EternalGoal>(json, options),
                    "Checklist" => JsonSerializer.Deserialize<ChecklistGoal>(json, options),
                    "Negative" => JsonSerializer.Deserialize<NegativeGoal>(json, options),
                    _ => throw new JsonException($"Unknown goal type: {type}")
                };
            }
        }

        public override void Write(Utf8JsonWriter writer, Goal value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
        }
    }
}