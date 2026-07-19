using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NJsonSchema;

namespace TaskFlow.ContractTests
{
    public sealed class OpenApiResponseValidator
    {
        private readonly OpenApiDocumentFixture _fixture;

        public OpenApiResponseValidator(OpenApiDocumentFixture fixture)
        {
            _fixture = fixture;
        }

        public async Task ValidateAsync(HttpResponseMessage response, string requestPath, OperationType operationType, int expectedStatusCode)
        {
            if ((int)response.StatusCode != expectedStatusCode)
            {
                throw new InvalidOperationException(
                    $"Expected status code {expectedStatusCode} but received {(int)response.StatusCode}.");
            }

            if (expectedStatusCode == StatusCodes.Status204NoContent)
            {
                if (response.Content.Headers.ContentLength.GetValueOrDefault() > 0)
                {
                    throw new InvalidOperationException("Expected no content for 204 response.");
                }

                return;
            }

            var responseDefinition = _fixture.GetResponse(requestPath, operationType, expectedStatusCode.ToString());
            var mediaType = responseDefinition.Content.Keys.FirstOrDefault();
            if (mediaType is null)
            {
                throw new InvalidOperationException($"Nenhum tipo de conteúdo definido para {expectedStatusCode} em {requestPath}.");
            }

            var actualMediaType = response.Content.Headers.ContentType?.MediaType;
            if (!string.Equals(actualMediaType, mediaType, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    $"Expected Content-Type '{mediaType}' but received '{actualMediaType}'.");
            }

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<object>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            });

            if (data is null)
            {
                throw new InvalidOperationException("Resposta JSON inválida ou vazia.");
            }

            var schema = responseDefinition.Content[mediaType].Schema;
            var jsonSchema = ConvertSchema(schema);
            var errors = jsonSchema.Validate(data);
            if (errors.Any())
            {
                var message = string.Join("; ", errors.Select(error => error.ToString()));
                throw new InvalidOperationException($"OpenAPI response validation failed: {message}");
            }
        }

        private JsonSchema ConvertSchema(OpenApiSchema schema)
        {
            var visited = new Dictionary<string, JsonSchema>();
            return ConvertSchemaInternal(schema, visited);
        }

        private JsonSchema ConvertSchemaInternal(OpenApiSchema schema, IDictionary<string, JsonSchema> visited)
        {
            if (schema.Reference != null)
            {
                var key = schema.Reference.Id;
                if (visited.TryGetValue(key, out var cached))
                {
                    return cached;
                }

                if (!_fixture.Document.Components.Schemas.TryGetValue(key, out var referencedSchema))
                {
                    throw new InvalidOperationException($"Referência de schema não encontrada: {key}");
                }

                var resolved = new JsonSchema();
                visited[key] = resolved;
                var converted = ConvertSchemaInternal(referencedSchema, visited);
                resolved = converted;
                visited[key] = resolved;
                return resolved;
            }

            var result = new JsonSchema
            {
                Type = ConvertType(schema.Type),
                Format = schema.Format,
                IsNullableRaw = schema.Nullable,
                AllowAdditionalProperties = schema.AdditionalPropertiesAllowed
            };

            if (schema.Enum?.Count > 0)
            {
                foreach (var item in schema.Enum)
                {
                    if (item is OpenApiString openApiString)
                    {
                        result.Enumeration.Add(openApiString.Value);
                    }
                    else if (item is OpenApiInteger openApiInteger)
                    {
                        result.Enumeration.Add(openApiInteger.Value);
                    }
                    else if (item is OpenApiBoolean openApiBoolean)
                    {
                        result.Enumeration.Add(openApiBoolean.Value);
                    }
                }
            }

            if (schema.Minimum.HasValue)
            {
                result.Minimum = schema.Minimum.Value;
            }

            if (schema.Maximum.HasValue)
            {
                result.Maximum = schema.Maximum.Value;
            }

            if (schema.MinLength.HasValue)
            {
                result.MinLength = schema.MinLength.Value;
            }

            if (schema.MaxLength.HasValue)
            {
                result.MaxLength = schema.MaxLength.Value;
            }

            if (!string.IsNullOrEmpty(schema.Pattern))
            {
                result.Pattern = schema.Pattern;
            }

            if (schema.Properties != null)
            {
                foreach (var property in schema.Properties)
                {
                    var propertySchema = new JsonSchemaProperty
                    {
                        ActualSchema = ConvertSchemaInternal(property.Value, visited)
                    };

                    result.Properties[property.Key] = propertySchema;
                }
            }

            if (schema.Required != null)
            {
                foreach (var requiredProperty in schema.Required)
                {
                    if (result.Properties.TryGetValue(requiredProperty, out var propertySchema))
                    {
                        propertySchema.IsRequired = true;
                    }
                }
            }

            if (schema.Items != null)
            {
                result.Item = ConvertSchemaInternal(schema.Items, visited);
            }

            if (schema.AdditionalProperties != null)
            {
                result.AdditionalPropertiesSchema = ConvertSchemaInternal(schema.AdditionalProperties, visited);
            }

            if (schema.AllOf != null && schema.AllOf.Count > 0)
            {
                foreach (var item in schema.AllOf)
                {
                    result.AllOf.Add(ConvertSchemaInternal(item, visited));
                }
            }

            return result;
        }

        private static JsonObjectType ConvertType(string? type)
        {
            return type switch
            {
                "object" => JsonObjectType.Object,
                "array" => JsonObjectType.Array,
                "string" => JsonObjectType.String,
                "integer" => JsonObjectType.Integer,
                "number" => JsonObjectType.Number,
                "boolean" => JsonObjectType.Boolean,
                _ => JsonObjectType.None
            };
        }
    }
}
