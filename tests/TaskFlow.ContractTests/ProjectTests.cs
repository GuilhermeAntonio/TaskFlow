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

        [Fact]
        public async Task CreateProject_Valid_Returns201_AndMatchesSchema()
        {
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var response = await client.PostAsJsonAsync("/projetos", new
            {
                name = "Projeto Contrato",
                description = "Descrição do projeto"
            });

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            await _validator.ValidateAsync(response, "/projetos", OperationType.Post, 201);

            var payload = await response.Content.ReadFromJsonAsync<JsonDocument>();
            Assert.NotNull(payload);
            Assert.True(payload.RootElement.TryGetProperty("id", out _));
            Assert.Equal("Projeto Contrato", payload.RootElement.GetProperty("name").GetString());
            Assert.Equal("active", payload.RootElement.GetProperty("status").GetString());
            Assert.Equal("Descrição do projeto", payload.RootElement.GetProperty("description").GetString());
            Assert.True(payload.RootElement.TryGetProperty("createdAt", out _));
        }

        [Fact]
        public async Task GetProject_NonExisting_Returns404_WithProblemDetailsCode()
        {
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var response = await client.GetAsync($"/projetos/{Guid.NewGuid():D}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            await _validator.ValidateAsync(response, "/projetos/{id}", OperationType.Get, 404);

            var body = await ReadJsonAsync(response);
            Assert.Equal("project_not_found", body.GetProperty("code").GetString());
        }

        [Fact]
        public async Task CreateProject_DuplicateName_Returns409_WithCode()
        {
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var payload = new { name = "Projeto Duplicado" };
            var first = await client.PostAsJsonAsync("/projetos", payload);
            first.EnsureSuccessStatusCode();

            var second = await client.PostAsJsonAsync("/projetos", payload);
            Assert.Equal(HttpStatusCode.Conflict, second.StatusCode);
            await _validator.ValidateAsync(second, "/projetos", OperationType.Post, 409);

            var body = await ReadJsonAsync(second);
            Assert.Equal("project_name_conflict", body.GetProperty("code").GetString());
        }

        [Fact]
        public async Task PatchProject_ArchivedWithInProgressTasks_Returns422()
        {
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var project = await client.PostAsJsonAsync("/projetos", new { name = "Projeto Arquiva" });
            project.EnsureSuccessStatusCode();
            var createdProject = await ReadJsonAsync(project);
            var projectId = createdProject.GetProperty("id").GetString();
            Assert.NotNull(projectId);

            var task = await client.PostAsJsonAsync($"/projetos/{projectId}/tarefas", new
            {
                title = "Tarefa 1",
                priority = "medium"
            });
            task.EnsureSuccessStatusCode();

            var patchResult = await client.PatchAsync($"/tarefas/{(await ReadJsonAsync(task)).GetProperty("id").GetString()}", JsonContent.Create(new { status = "in_progress" }));
            patchResult.EnsureSuccessStatusCode();

            var archiveResult = await client.PatchAsync($"/projetos/{projectId}", JsonContent.Create(new { status = "archived" }));
            Assert.Equal((HttpStatusCode)422, archiveResult.StatusCode);
            await _validator.ValidateAsync(archiveResult, "/projetos/{id}", OperationType.Patch, 422);

            var body = await ReadJsonAsync(archiveResult);
            Assert.Equal("project_has_in_progress_tasks", body.GetProperty("code").GetString());
        }

        [Fact]
        public async Task PatchProject_EmptyPayload_Returns400_ValidationProblemDetails()
        {
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var project = await client.PostAsJsonAsync("/projetos", new { name = "Projeto Patch" });
            project.EnsureSuccessStatusCode();
            var projectId = (await ReadJsonAsync(project)).GetProperty("id").GetString();
            Assert.NotNull(projectId);

            var patchResult = await client.PatchAsync($"/projetos/{projectId}", JsonContent.Create(new { }));
            Assert.Equal(HttpStatusCode.BadRequest, patchResult.StatusCode);
            await _validator.ValidateAsync(patchResult, "/projetos/{id}", OperationType.Patch, 400);

            var body = await ReadJsonAsync(patchResult);
            Assert.Equal("validation_error", body.GetProperty("code").GetString());
            Assert.True(body.GetProperty("errors").TryGetProperty("requestBody", out _));
        }

        private static async Task<JsonElement> ReadJsonAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            using var document = JsonDocument.Parse(content);
            return document.RootElement.Clone();
        }

        private static TaskFlowApiFactory CreateFactory()
        {
            var path = Path.Combine(Path.GetTempPath(), $"taskflow_contract_{Guid.NewGuid():N}.db");
            return new TaskFlowApiFactory(path);
        }
    }
}
