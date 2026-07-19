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
    public class TaskTests : IClassFixture<OpenApiDocumentFixture>
    {
        private readonly OpenApiDocumentFixture _fixture;
        private readonly OpenApiResponseValidator _validator;

        public TaskTests(OpenApiDocumentFixture fixture)
        {
            _fixture = fixture;
            _validator = new OpenApiResponseValidator(fixture);
        }

        [Fact]
        public async Task CreateTask_Valid_Returns201_AndMatchesSchema()
        {
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var project = await client.PostAsJsonAsync("/projetos", new { name = "Projeto Tarefa" });
            project.EnsureSuccessStatusCode();
            var projectId = (await ReadJsonAsync(project)).GetProperty("id").GetString();
            Assert.NotNull(projectId);

            var response = await client.PostAsJsonAsync($"/projetos/{projectId}/tarefas", new
            {
                title = "Implementar validação",
                priority = "high"
            });

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            await _validator.ValidateAsync(response, "/projetos/{id}/tarefas", OperationType.Post, 201);

            var body = await ReadJsonAsync(response);
            Assert.Equal("Implementar validação", body.GetProperty("title").GetString());
            Assert.Equal("high", body.GetProperty("priority").GetString());
            Assert.Equal("pending", body.GetProperty("status").GetString());
            Assert.Equal(projectId, body.GetProperty("projectId").GetString());
        }

        [Fact]
        public async Task CreateTask_DuplicateTitle_Returns409_WithCode()
        {
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var project = await client.PostAsJsonAsync("/projetos", new { name = "Projeto Task Dup" });
            project.EnsureSuccessStatusCode();
            var projectId = (await ReadJsonAsync(project)).GetProperty("id").GetString();
            Assert.NotNull(projectId);

            await client.PostAsJsonAsync($"/projetos/{projectId}/tarefas", new { title = "Duplicada", priority = "medium" });

            var duplicate = await client.PostAsJsonAsync($"/projetos/{projectId}/tarefas", new { title = "Duplicada", priority = "low" });
            Assert.Equal(HttpStatusCode.Conflict, duplicate.StatusCode);
            await _validator.ValidateAsync(duplicate, "/projetos/{id}/tarefas", OperationType.Post, 409);
            Assert.Equal("task_title_conflict", (await ReadJsonAsync(duplicate)).GetProperty("code").GetString());
        }

        [Fact]
        public async Task CreateTask_OnArchivedProject_Returns422_WithCode()
        {
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var project = await client.PostAsJsonAsync("/projetos", new { name = "Projeto Arquivado" });
            project.EnsureSuccessStatusCode();
            var projectId = (await ReadJsonAsync(project)).GetProperty("id").GetString();
            Assert.NotNull(projectId);

            var archive = await client.PatchAsync($"/projetos/{projectId}", JsonContent.Create(new { status = "archived" }));
            archive.EnsureSuccessStatusCode();

            var response = await client.PostAsJsonAsync($"/projetos/{projectId}/tarefas", new { title = "Nova tarefa", priority = "low" });
            Assert.Equal((HttpStatusCode)422, response.StatusCode);
            await _validator.ValidateAsync(response, "/projetos/{id}/tarefas", OperationType.Post, 422);
            Assert.Equal("archived_project_does_not_accept_tasks", (await ReadJsonAsync(response)).GetProperty("code").GetString());
        }

        [Fact]
        public async Task ListTasks_FilterByStatusPriority_ReturnsMatchingTasks()
        {
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var project = await client.PostAsJsonAsync("/projetos", new { name = "Projeto Filter" });
            project.EnsureSuccessStatusCode();
            var projectId = (await ReadJsonAsync(project)).GetProperty("id").GetString();
            Assert.NotNull(projectId);

            var create1 = await client.PostAsJsonAsync($"/projetos/{projectId}/tarefas", new { title = "Tarefa A", priority = "low" });
            create1.EnsureSuccessStatusCode();
            var taskA = await ReadJsonAsync(create1);

            var create2 = await client.PostAsJsonAsync($"/projetos/{projectId}/tarefas", new { title = "Tarefa B", priority = "high" });
            create2.EnsureSuccessStatusCode();
            var taskBId = (await ReadJsonAsync(create2)).GetProperty("id").GetString();
            Assert.NotNull(taskBId);

            var patch = await client.PatchAsync($"/tarefas/{taskBId}", JsonContent.Create(new { status = "in_progress" }));
            patch.EnsureSuccessStatusCode();

            var response = await client.GetAsync($"/projetos/{projectId}/tarefas?status=in_progress&priority=high");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            await _validator.ValidateAsync(response, "/projetos/{id}/tarefas", OperationType.Get, 200);

            var list = await ReadJsonAsync(response);
            Assert.Equal(1, list.GetArrayLength());
            Assert.Equal("Tarefa B", list[0].GetProperty("title").GetString());
        }

        [Fact]
        public async Task PatchTask_UpdateFieldsAndStatusToDone_Returns200_AndCompletedAtSet()
        {
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var project = await client.PostAsJsonAsync("/projetos", new { name = "Projeto Patch Task" });
            project.EnsureSuccessStatusCode();
            var projectId = (await ReadJsonAsync(project)).GetProperty("id").GetString();
            Assert.NotNull(projectId);

            var create = await client.PostAsJsonAsync($"/projetos/{projectId}/tarefas", new { title = "Tarefa Patch", priority = "medium" });
            create.EnsureSuccessStatusCode();
            var taskId = (await ReadJsonAsync(create)).GetProperty("id").GetString();
            Assert.NotNull(taskId);

            var patch1 = await client.PatchAsync($"/tarefas/{taskId}", JsonContent.Create(new { status = "in_progress" }));
            patch1.EnsureSuccessStatusCode();
            await _validator.ValidateAsync(patch1, "/tarefas/{id}", OperationType.Patch, 200);

            var patch2 = await client.PatchAsync($"/tarefas/{taskId}", JsonContent.Create(new { status = "done" }));
            patch2.EnsureSuccessStatusCode();
            var doneTask = await ReadJsonAsync(patch2);
            Assert.Equal("done", doneTask.GetProperty("status").GetString());
            Assert.True(doneTask.GetProperty("completedAt").GetString() is not null);
        }

        [Fact]
        public async Task PatchTask_DoneWithExtraField_Returns422_CompletedTaskCannotBeModified()
        {
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var project = await client.PostAsJsonAsync("/projetos", new { name = "Projeto Done" });
            project.EnsureSuccessStatusCode();
            var projectId = (await ReadJsonAsync(project)).GetProperty("id").GetString();
            Assert.NotNull(projectId);

            var create = await client.PostAsJsonAsync($"/projetos/{projectId}/tarefas", new { title = "Tarefa Done", priority = "low" });
            create.EnsureSuccessStatusCode();
            var taskId = (await ReadJsonAsync(create)).GetProperty("id").GetString();
            Assert.NotNull(taskId);

            var patch1 = await client.PatchAsync($"/tarefas/{taskId}", JsonContent.Create(new { status = "in_progress" }));
            patch1.EnsureSuccessStatusCode();
            var patch2 = await client.PatchAsync($"/tarefas/{taskId}", JsonContent.Create(new { status = "done" }));
            patch2.EnsureSuccessStatusCode();

            var invalidPatch = await client.PatchAsync($"/tarefas/{taskId}", JsonContent.Create(new { status = "done", title = "Novo Título" }));
            Assert.Equal((HttpStatusCode)422, invalidPatch.StatusCode);
            await _validator.ValidateAsync(invalidPatch, "/tarefas/{id}", OperationType.Patch, 422);
            Assert.Equal("completed_task_cannot_be_modified", (await ReadJsonAsync(invalidPatch)).GetProperty("code").GetString());
        }

        [Fact]
        public async Task DeleteTask_Pending_Returns204()
        {
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var project = await client.PostAsJsonAsync("/projetos", new { name = "Projeto Delete" });
            project.EnsureSuccessStatusCode();
            var projectId = (await ReadJsonAsync(project)).GetProperty("id").GetString();
            Assert.NotNull(projectId);

            var create = await client.PostAsJsonAsync($"/projetos/{projectId}/tarefas", new { title = "Tarefa Delete", priority = "medium" });
            create.EnsureSuccessStatusCode();
            var taskId = (await ReadJsonAsync(create)).GetProperty("id").GetString();
            Assert.NotNull(taskId);

            var delete = await client.DeleteAsync($"/tarefas/{taskId}");
            Assert.Equal(HttpStatusCode.NoContent, delete.StatusCode);
            Assert.Equal(0, delete.Content.Headers.ContentLength.GetValueOrDefault());
        }

        [Fact]
        public async Task DeleteTask_InProgress_Returns422_WithCode()
        {
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var project = await client.PostAsJsonAsync("/projetos", new { name = "Projeto Delete2" });
            project.EnsureSuccessStatusCode();
            var projectId = (await ReadJsonAsync(project)).GetProperty("id").GetString();
            Assert.NotNull(projectId);

            var create = await client.PostAsJsonAsync($"/projetos/{projectId}/tarefas", new { title = "Tarefa Não Excluível", priority = "medium" });
            create.EnsureSuccessStatusCode();
            var taskId = (await ReadJsonAsync(create)).GetProperty("id").GetString();
            Assert.NotNull(taskId);

            var patch = await client.PatchAsync($"/tarefas/{taskId}", JsonContent.Create(new { status = "in_progress" }));
            patch.EnsureSuccessStatusCode();

            var delete = await client.DeleteAsync($"/tarefas/{taskId}");
            Assert.Equal((HttpStatusCode)422, delete.StatusCode);
            await _validator.ValidateAsync(delete, "/tarefas/{id}", OperationType.Delete, 422);
            Assert.Equal("task_cannot_be_deleted", (await ReadJsonAsync(delete)).GetProperty("code").GetString());
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
