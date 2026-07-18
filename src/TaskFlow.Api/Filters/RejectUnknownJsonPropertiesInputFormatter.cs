using System.Buffers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace TaskFlow.Api.Filters
{
    public sealed class RejectUnknownJsonPropertiesInputFormatter : TextInputFormatter
    {
        private readonly JsonSerializerOptions _serializerOptions;

        public RejectUnknownJsonPropertiesInputFormatter(JsonSerializerOptions serializerOptions)
        {
            _serializerOptions = serializerOptions;
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/json"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override bool CanRead(InputFormatterContext context)
        {
            return base.CanRead(context);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            var request = context.HttpContext.Request;
            request.EnableBuffering();
            request.Body.Position = 0;

            string body;
            using (var reader = new StreamReader(request.Body, encoding, detectEncodingFromByteOrderMarks: false, leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
            }

            request.Body.Position = 0;

            if (!string.IsNullOrWhiteSpace(body))
            {
                try
                {
                    using var document = JsonDocument.Parse(body);
                    if (document.RootElement.ValueKind == JsonValueKind.Object)
                    {
                        var allowedProperties = GetAllowedPropertyNames(context.ModelType);
                        var unknownProperties = document.RootElement.EnumerateObject()
                            .Select(p => p.Name)
                            .Where(name => !allowedProperties.Contains(name, StringComparer.Ordinal))
                            .ToArray();

                        if (unknownProperties.Length > 0)
                        {
                            foreach (var unknown in unknownProperties)
                            {
                                context.ModelState.TryAddModelError(unknown, "A propriedade não é permitida pelo contrato.");
                            }

                            return await InputFormatterResult.FailureAsync();
                        }
                    }
                }
                catch (JsonException)
                {
                    // Let the default formatter handle malformed JSON.
                }
            }

            try
            {
                var model = JsonSerializer.Deserialize(body, context.ModelType, _serializerOptions);
                return await InputFormatterResult.SuccessAsync(model);
            }
            catch (JsonException jsonException)
            {
                context.ModelState.TryAddModelError(string.Empty, jsonException.Message);
                return await InputFormatterResult.FailureAsync();
            }
        }

        private static IReadOnlySet<string> GetAllowedPropertyNames(Type modelType)
        {
            var allowedProperties = new HashSet<string>(StringComparer.Ordinal);
            var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            foreach (var property in modelType.GetProperties(bindingFlags))
            {
                if (property.GetCustomAttribute<JsonIgnoreAttribute>() != null)
                {
                    continue;
                }

                var jsonPropertyName = property.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name;
                if (!string.IsNullOrWhiteSpace(jsonPropertyName))
                {
                    allowedProperties.Add(jsonPropertyName);
                    continue;
                }

                allowedProperties.Add(JsonNamingPolicy.CamelCase.ConvertName(property.Name));
            }

            return allowedProperties;
        }
    }
}
