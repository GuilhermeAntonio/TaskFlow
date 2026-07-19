using System;
using System.IO;
using System.Linq;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace TaskFlow.ContractTests
{
    public sealed class OpenApiDocumentFixture : IDisposable
    {
        public OpenApiDocument Document { get; }

        public OpenApiDocumentFixture()
        {
            var root = FindRepositoryRoot();
            var contractPath = Path.Combine(root, "openapi.yaml");

            using var stream = File.OpenRead(contractPath);
            var reader = new OpenApiStreamReader();
            Document = reader.Read(stream, out var diagnostic);

            if (diagnostic?.Errors?.Any() == true)
            {
                throw new InvalidOperationException(
                    $"OpenAPI document contains errors: {string.Join("; ", diagnostic.Errors.Select(e => e.Message))}");
            }
        }

        private static string FindRepositoryRoot()
        {
            var current = new DirectoryInfo(AppContext.BaseDirectory);
            while (current is not null && !File.Exists(Path.Combine(current.FullName, "openapi.yaml")))
            {
                current = current.Parent;
            }

            return current?.FullName
                ?? throw new InvalidOperationException("Não foi possível localizar o arquivo openapi.yaml no diretório pai.");
        }

        public OpenApiPathItem GetPathItem(string requestPath)
        {
            var normalizedRequestPath = NormalizePath(requestPath);
            var candidate = Document.Paths.FirstOrDefault(item => PathMatches(item.Key, normalizedRequestPath));
            if (candidate.Key is null)
            {
                throw new InvalidOperationException($"Caminho OpenAPI não encontrado para '{requestPath}'.");
            }

            return candidate.Value;
        }

        public OpenApiOperation GetOperation(string requestPath, OperationType operationType)
        {
            var pathItem = GetPathItem(requestPath);
            if (!pathItem.Operations.TryGetValue(operationType, out var operation))
            {
                throw new InvalidOperationException($"Operação {operationType} não encontrada para '{requestPath}'.");
            }

            return operation;
        }

        public OpenApiResponse GetResponse(string requestPath, OperationType operationType, string statusCode)
        {
            var operation = GetOperation(requestPath, operationType);
            if (!operation.Responses.TryGetValue(statusCode, out var response))
            {
                throw new InvalidOperationException(
                    $"Resposta {statusCode} não encontrada para {operationType} {requestPath}.");
            }

            return response;
        }

        private static string NormalizePath(string path)
        {
            return path.TrimEnd('/');
        }

        private static bool PathMatches(string template, string actual)
        {
            var templateSegments = template.Trim('/').Split('/');
            var actualSegments = actual.Trim('/').Split('/');

            if (templateSegments.Length != actualSegments.Length)
            {
                return false;
            }

            for (var index = 0; index < templateSegments.Length; index++)
            {
                var templateSegment = templateSegments[index];
                var actualSegment = actualSegments[index];

                if (templateSegment.StartsWith("{", StringComparison.Ordinal) &&
                    templateSegment.EndsWith("}", StringComparison.Ordinal))
                {
                    continue;
                }

                if (!string.Equals(templateSegment, actualSegment, StringComparison.Ordinal))
                {
                    return false;
                }
            }

            return true;
        }

        public void Dispose()
        {
        }
    }
}
