using System.Text.Json.Serialization;

namespace TaskFlow.Api.Contracts
{
    [JsonConverter(typeof(OptionalFieldJsonConverterFactory))]
    public readonly struct OptionalField<T>
    {
        public bool HasValue { get; }
        public T? Value { get; }

        public OptionalField(T? value)
        {
            HasValue = true;
            Value = value;
        }

        public static OptionalField<T> NotSpecified => default;

        public static implicit operator OptionalField<T>(T? value) => new OptionalField<T>(value);
    }
}
