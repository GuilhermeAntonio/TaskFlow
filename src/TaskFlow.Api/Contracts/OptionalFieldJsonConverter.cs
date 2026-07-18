using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskFlow.Api.Contracts
{
    public sealed class OptionalFieldJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsGenericType &&
                typeToConvert.GetGenericTypeDefinition() == typeof(OptionalField<>);
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var itemType = typeToConvert.GetGenericArguments()[0];
            var converterType = typeof(OptionalFieldJsonConverter<>).MakeGenericType(itemType);
            return (JsonConverter?)Activator.CreateInstance(converterType);
        }
    }

    public sealed class OptionalFieldJsonConverter<T> : JsonConverter<OptionalField<T>>
    {
        public override OptionalField<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = JsonSerializer.Deserialize<T?>(ref reader, options);
            return new OptionalField<T>(value);
        }

        public override void Write(Utf8JsonWriter writer, OptionalField<T> value, JsonSerializerOptions options)
        {
            if (!value.HasValue)
            {
                writer.WriteNullValue();
                return;
            }

            JsonSerializer.Serialize(writer, value.Value, options);
        }
    }
}
