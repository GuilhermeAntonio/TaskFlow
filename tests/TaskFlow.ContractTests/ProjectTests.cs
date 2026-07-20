using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Xunit;

namespace TaskFlow.ContractTests
{
    public class ProjectTests : IClassFixture<OpenApiDocumentFixture>
    {
        private readonly OpenApiDocumentFixture _fixture;
        private readonly OpenApiResponseValidator _validator;

        public ProjectTests(OpenApiDocumentFixture fixture)
        {
            _fixture = fixture;
            _validator = new OpenApiResponseValidator(fixture);
        }

        /// <summary>
        /// Verifica se a criação de um projeto com dados válidos retorna
        /// 201 Created e uma resposta aderente ao contrato OpenAPI.
        /// </summary>
        [Fact]
        public async Task CreateProject_Valid_Returns201_AndMatchesSchema()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync(
                "/projetos",
                new
                {
                    name = "Projeto Contrato",
                    description = "Descrição do projeto"
                });

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/projetos",
                OperationType.Post,
                201);

            var payload = await response.Content.ReadFromJsonAsync<JsonDocument>();

            Assert.NotNull(payload);
            Assert.True(payload.RootElement.TryGetProperty("id", out _));
            Assert.Equal("Projeto Contrato", payload.RootElement.GetProperty("name").GetString());
            Assert.Equal("active", payload.RootElement.GetProperty("status").GetString());
            Assert.Equal("Descrição do projeto", payload.RootElement.GetProperty("description").GetString());
            Assert.True(payload.RootElement.TryGetProperty("createdAt", out _));
        }

        /// <summary>
        /// Verifica se a consulta de um projeto inexistente retorna
        /// 404 Not Found e o código project_not_found.
        /// </summary>
        [Fact]
        public async Task GetProject_NonExisting_Returns404_WithProblemDetailsCode()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = Guid.NewGuid();

            // Act
            var response = await client.GetAsync($"/projetos/{projectId:D}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/projetos/{id}",
                OperationType.Get,
                404);

            var body = await ReadJsonAsync(response);

            Assert.Equal(
                "project_not_found",
                body.GetProperty("code").GetString());
        }

        /// <summary>
        /// Verifica se a criação de um projeto com nome já existente
        /// retorna 409 Conflict e o código project_name_conflict.
        /// </summary>
        [Fact]
        public async Task CreateProject_DuplicateName_Returns409_WithCode()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var payload = new
            {
                name = "Projeto Duplicado"
            };

            var firstResponse = await client.PostAsJsonAsync("/projetos", payload);
            firstResponse.EnsureSuccessStatusCode();

            // Act
            var secondResponse = await client.PostAsJsonAsync("/projetos", payload);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, secondResponse.StatusCode);

            await _validator.ValidateAsync(
                secondResponse,
                "/projetos",
                OperationType.Post,
                409);

            var body = await ReadJsonAsync(secondResponse);

            Assert.Equal(
                "project_name_conflict",
                body.GetProperty("code").GetString());
        }

        /// <summary>
        /// Verifica se a tentativa de arquivar um projeto que possui uma tarefa
        /// em andamento retorna 422 Unprocessable Entity e o código
        /// project_has_in_progress_tasks.
        /// </summary>
        [Fact]
        public async Task PatchProject_ArchivedWithInProgressTasks_Returns422()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectResponse = await client.PostAsJsonAsync(
                "/projetos",
                new
                {
                    name = "Projeto Arquiva"
                });

            projectResponse.EnsureSuccessStatusCode();

            var projectId = (await ReadJsonAsync(projectResponse))
                .GetProperty("id")
                .GetString();

            Assert.NotNull(projectId);

            var taskResponse = await client.PostAsJsonAsync(
                $"/projetos/{projectId}/tarefas",
                new
                {
                    title = "Tarefa 1",
                    priority = "medium"
                });

            taskResponse.EnsureSuccessStatusCode();

            var taskId = (await ReadJsonAsync(taskResponse))
                .GetProperty("id")
                .GetString();

            Assert.NotNull(taskId);

            var startTaskResponse = await client.PatchAsync(
                $"/tarefas/{taskId}",
                JsonContent.Create(new
                {
                    status = "in_progress"
                }));

            startTaskResponse.EnsureSuccessStatusCode();

            // Act
            var archiveResponse = await client.PatchAsync(
                $"/projetos/{projectId}",
                JsonContent.Create(new
                {
                    status = "archived"
                }));

            // Assert
            Assert.Equal((HttpStatusCode)422, archiveResponse.StatusCode);

            await _validator.ValidateAsync(
                archiveResponse,
                "/projetos/{id}",
                OperationType.Patch,
                422);

            var body = await ReadJsonAsync(archiveResponse);

            Assert.Equal(
                "project_has_in_progress_tasks",
                body.GetProperty("code").GetString());
        }

        /// <summary>
        /// Verifica se o envio de um PATCH de projeto sem campos para atualização
        /// retorna 400 Bad Request, o código validation_error e o erro requestBody.
        /// </summary>
        [Fact]
        public async Task PatchProject_EmptyPayload_Returns400_ValidationProblemDetails()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectResponse = await client.PostAsJsonAsync(
                "/projetos",
                new
                {
                    name = "Projeto Patch"
                });

            projectResponse.EnsureSuccessStatusCode();

            var projectId = (await ReadJsonAsync(projectResponse))
                .GetProperty("id")
                .GetString();

            Assert.NotNull(projectId);

            // Act
            var patchResponse = await client.PatchAsync(
                $"/projetos/{projectId}",
                JsonContent.Create(new { }));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, patchResponse.StatusCode);

            await _validator.ValidateAsync(
                patchResponse,
                "/projetos/{id}",
                OperationType.Patch,
                400);

            var body = await ReadJsonAsync(patchResponse);

            Assert.Equal(
                "validation_error",
                body.GetProperty("code").GetString());

            Assert.True(
                body.GetProperty("errors")
                    .TryGetProperty("requestBody", out _));
        }

        /// <summary>
        /// Verifica se a tentativa de alterar um projeto inexistente retorna
        /// 404 Not Found e o código project_not_found.
        /// </summary>
        [Fact]
        public async Task PatchProject_NonExisting_Returns404_WithCode()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = Guid.NewGuid();

            // Act
            var response = await client.PatchAsync(
                $"/projetos/{projectId:D}",
                JsonContent.Create(new
                {
                    description = "Descrição atualizada"
                }));

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/projetos/{id}",
                OperationType.Patch,
                404);

            var body = await ReadJsonAsync(response);

            Assert.Equal(
                "project_not_found",
                body.GetProperty("code").GetString());
        }

        /// <summary>
        /// Verifica se a consulta de um projeto com UUID malformado retorna
        /// 400 Bad Request e uma resposta aderente ao contrato OpenAPI.
        /// </summary>
        [Fact]
        public async Task GetProject_MalformedUuid_Returns400_AndMatchesSchema()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/projetos/not-a-guid");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/projetos/{id}",
                OperationType.Get,
                400);

            var body = await ReadJsonAsync(response);

            Assert.Equal(
                "validation_error",
                body.GetProperty("code").GetString());

            Assert.True(
                body.GetProperty("errors")
                    .TryGetProperty("id", out _));
        }

        private static async Task<JsonElement> ReadJsonAsync(
            HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(content);

            return document.RootElement.Clone();
        }

        private static TaskFlowApiFactory CreateFactory()
        {
            var path = Path.Combine(
                Path.GetTempPath(),
                $"taskflow_contract_{Guid.NewGuid():N}.db");

            return new TaskFlowApiFactory(path);
        }
    }
}