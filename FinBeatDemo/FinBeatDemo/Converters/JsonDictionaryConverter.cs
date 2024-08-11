using System.Text.Json.Serialization;
using System.Text.Json;

namespace FinBeatDemo.Converters;

public class JsonDictionaryConverter : JsonConverter<Dictionary<int, string>>
{
    public override Dictionary<int, string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Failed to parse an array start token.");
        }

        var dictionary = new Dictionary<int, string>();

        while (true)
        {
            if (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    if (reader.Read())
                    {
                        if (reader.TokenType == JsonTokenType.PropertyName)
                        {
                            var propertyName = reader.GetString();

                            if (propertyName != null)
                            {
                                /* Not gonna catch this, I already added too many throws I aint gonna need. */
                                var key = int.Parse(propertyName);

                                if (reader.Read())
                                {
                                    string? value = reader.GetString();

                                    if(value != null)
                                    {
                                        dictionary.Add(key, value);

                                        if (reader.Read())
                                        {
                                            if (reader.TokenType != JsonTokenType.EndObject)
                                            {
                                                throw new JsonException("Unexpected token found, expected an object end instead.");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new JsonException("A property value is null.");
                                    }
                                }
                            }
                            else
                            {
                                throw new JsonException("A property name is null.");
                            }
                        }
                        else
                        {
                            throw new JsonException("Unexpected token found, expected a property name instead.");
                        }
                    }
                }
                else if(reader.TokenType == JsonTokenType.EndArray)
                {
                    return dictionary;
                }
                else
                {
                    throw new JsonException("Unexpected token found, expected either an object start or an array end.");
                }
            }
        }

        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<int, string> dictionary, JsonSerializerOptions options)
    {
        /* For this one I am only gonna need 'READ'. */
        throw new NotImplementedException();
    }
}