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

        /// <summary>
        /// Verifica se a criação de uma tarefa com dados válidos retorna
        /// 201 Created e uma resposta aderente ao contrato OpenAPI.
        /// </summary>
        [Fact]
        public async Task CreateTask_Valid_Returns201_AndMatchesSchema()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = await CreateProjectAsync(
                client,
                "Projeto Tarefa");

            // Act
            var response = await client.PostAsJsonAsync(
                $"/projetos/{projectId}/tarefas",
                new
                {
                    title = "Implementar validação",
                    priority = "high"
                });

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/projetos/{id}/tarefas",
                OperationType.Post,
                201);

            var body = await ReadJsonAsync(response);

            Assert.Equal(
                "Implementar validação",
                body.GetProperty("title").GetString());

            Assert.Equal(
                "high",
                body.GetProperty("priority").GetString());

            Assert.Equal(
                "pending",
                body.GetProperty("status").GetString());

            Assert.Equal(
                projectId,
                body.GetProperty("projectId").GetString());
        }

        /// <summary>
        /// Verifica se a criação de uma tarefa com título já existente
        /// no mesmo projeto retorna 409 Conflict e o código task_title_conflict.
        /// </summary>
        [Fact]
        public async Task CreateTask_DuplicateTitle_Returns409_WithCode()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = await CreateProjectAsync(
                client,
                "Projeto Task Dup");

            var firstTaskResponse = await client.PostAsJsonAsync(
                $"/projetos/{projectId}/tarefas",
                new
                {
                    title = "Duplicada",
                    priority = "medium"
                });

            firstTaskResponse.EnsureSuccessStatusCode();

            // Act
            var duplicateResponse = await client.PostAsJsonAsync(
                $"/projetos/{projectId}/tarefas",
                new
                {
                    title = "Duplicada",
                    priority = "low"
                });

            // Assert
            Assert.Equal(
                HttpStatusCode.Conflict,
                duplicateResponse.StatusCode);

            await _validator.ValidateAsync(
                duplicateResponse,
                "/projetos/{id}/tarefas",
                OperationType.Post,
                409);

            var body = await ReadJsonAsync(duplicateResponse);

            Assert.Equal(
                "task_title_conflict",
                body.GetProperty("code").GetString());
        }

        /// <summary>
        /// Verifica se a tentativa de criar uma tarefa em um projeto arquivado
        /// retorna 422 Unprocessable Entity e o código
        /// archived_project_does_not_accept_tasks.
        /// </summary>
        [Fact]
        public async Task CreateTask_OnArchivedProject_Returns422_WithCode()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = await CreateProjectAsync(
                client,
                "Projeto Arquivado");

            var archiveResponse = await client.PatchAsync(
                $"/projetos/{projectId}",
                JsonContent.Create(new
                {
                    status = "archived"
                }));

            archiveResponse.EnsureSuccessStatusCode();

            // Act
            var response = await client.PostAsJsonAsync(
                $"/projetos/{projectId}/tarefas",
                new
                {
                    title = "Nova tarefa",
                    priority = "low"
                });

            // Assert
            Assert.Equal((HttpStatusCode)422, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/projetos/{id}/tarefas",
                OperationType.Post,
                422);

            var body = await ReadJsonAsync(response);

            Assert.Equal(
                "archived_project_does_not_accept_tasks",
                body.GetProperty("code").GetString());
        }

        /// <summary>
        /// Verifica se a tentativa de criar uma tarefa em um projeto inexistente
        /// retorna 404 Not Found e o código project_not_found.
        /// </summary>
        [Fact]
        public async Task CreateTask_NonExistingProject_Returns404_WithCode()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = Guid.NewGuid();

            // Act
            var response = await client.PostAsJsonAsync(
                $"/projetos/{projectId:D}/tarefas",
                new
                {
                    title = "Tarefa sem projeto",
                    priority = "medium"
                });

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/projetos/{id}/tarefas",
                OperationType.Post,
                404);

            var body = await ReadJsonAsync(response);

            Assert.Equal(
                "project_not_found",
                body.GetProperty("code").GetString());
        }

        /// <summary>
        /// Verifica se a listagem de tarefas com filtros de status e prioridade
        /// retorna somente as tarefas correspondentes e uma resposta aderente
        /// ao contrato OpenAPI.
        /// </summary>
        [Fact]
        public async Task ListTasks_FilterByStatusPriority_ReturnsMatchingTasks()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = await CreateProjectAsync(
                client,
                "Projeto Filter");

            var firstTaskResponse = await client.PostAsJsonAsync(
                $"/projetos/{projectId}/tarefas",
                new
                {
                    title = "Tarefa A",
                    priority = "low"
                });

            firstTaskResponse.EnsureSuccessStatusCode();

            var secondTaskId = await CreateTaskAsync(
                client,
                projectId,
                "Tarefa B",
                "high");

            var patchResponse = await client.PatchAsync(
                $"/tarefas/{secondTaskId}",
                JsonContent.Create(new
                {
                    status = "in_progress"
                }));

            patchResponse.EnsureSuccessStatusCode();

            // Act
            var response = await client.GetAsync(
                $"/projetos/{projectId}/tarefas" +
                "?status=in_progress&priority=high");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/projetos/{id}/tarefas",
                OperationType.Get,
                200);

            var list = await ReadJsonAsync(response);

            Assert.Equal(1, list.GetArrayLength());

            Assert.Equal(
                "Tarefa B",
                list[0].GetProperty("title").GetString());
        }

        /// <summary>
        /// Verifica se a tentativa de listar tarefas de um projeto inexistente
        /// retorna 404 Not Found e o código project_not_found.
        /// </summary>
        [Fact]
        public async Task ListTasks_NonExistingProject_Returns404_WithCode()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = Guid.NewGuid();

            // Act
            var response = await client.GetAsync(
                $"/projetos/{projectId:D}/tarefas");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/projetos/{id}/tarefas",
                OperationType.Get,
                404);

            var body = await ReadJsonAsync(response);

            Assert.Equal(
                "project_not_found",
                body.GetProperty("code").GetString());
        }

        /// <summary>
        /// Verifica se uma tarefa pode seguir a transição válida de pending
        /// para in_progress e depois para done, retornando 200 OK e preenchendo
        /// automaticamente o campo completedAt.
        /// </summary>
        [Fact]
        public async Task PatchTask_UpdateFieldsAndStatusToDone_Returns200_AndCompletedAtSet()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = await CreateProjectAsync(
                client,
                "Projeto Patch Task");

            var taskId = await CreateTaskAsync(
                client,
                projectId,
                "Tarefa Patch");

            var startResponse = await client.PatchAsync(
                $"/tarefas/{taskId}",
                JsonContent.Create(new
                {
                    status = "in_progress"
                }));

            startResponse.EnsureSuccessStatusCode();

            await _validator.ValidateAsync(
                startResponse,
                "/tarefas/{id}",
                OperationType.Patch,
                200);

            // Act
            var doneResponse = await client.PatchAsync(
                $"/tarefas/{taskId}",
                JsonContent.Create(new
                {
                    status = "done"
                }));

            // Assert
            Assert.Equal(HttpStatusCode.OK, doneResponse.StatusCode);

            await _validator.ValidateAsync(
                doneResponse,
                "/tarefas/{id}",
                OperationType.Patch,
                200);

            var doneTask = await ReadJsonAsync(doneResponse);

            Assert.Equal(
                "done",
                doneTask.GetProperty("status").GetString());

            Assert.NotNull(
                doneTask.GetProperty("completedAt").GetString());
        }

        /// <summary>
        /// Verifica se a tentativa de alterar campos de uma tarefa concluída
        /// retorna 422 Unprocessable Entity e o código
        /// completed_task_cannot_be_modified.
        /// </summary>
        [Fact]
        public async Task PatchTask_DoneWithExtraField_Returns422_CompletedTaskCannotBeModified()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = await CreateProjectAsync(
                client,
                "Projeto Done");

            var taskId = await CreateTaskAsync(
                client,
                projectId,
                "Tarefa Done",
                "low");

            await MoveTaskToDoneAsync(client, taskId);

            // Act
            var invalidPatchResponse = await client.PatchAsync(
                $"/tarefas/{taskId}",
                JsonContent.Create(new
                {
                    status = "done",
                    title = "Novo Título"
                }));

            // Assert
            Assert.Equal(
                (HttpStatusCode)422,
                invalidPatchResponse.StatusCode);

            await _validator.ValidateAsync(
                invalidPatchResponse,
                "/tarefas/{id}",
                OperationType.Patch,
                422);

            var body = await ReadJsonAsync(invalidPatchResponse);

            Assert.Equal(
                "completed_task_cannot_be_modified",
                body.GetProperty("code").GetString());
        }

        /// <summary>
        /// Verifica se a tentativa de alterar uma tarefa inexistente retorna
        /// 404 Not Found e o código task_not_found.
        /// </summary>
        [Fact]
        public async Task PatchTask_NonExisting_Returns404_WithCode()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var taskId = Guid.NewGuid();

            // Act
            var response = await client.PatchAsync(
                $"/tarefas/{taskId:D}",
                JsonContent.Create(new
                {
                    priority = "high"
                }));

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/tarefas/{id}",
                OperationType.Patch,
                404);

            var body = await ReadJsonAsync(response);

            Assert.Equal(
                "task_not_found",
                body.GetProperty("code").GetString());
        }

        /// <summary>
        /// Verifica se a transição direta de pending para done retorna
        /// 422 Unprocessable Entity e o código invalid_task_status_transition.
        /// </summary>
        [Fact]
        public async Task PatchTask_PendingToDone_Returns422_WithCode()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = await CreateProjectAsync(
                client,
                "Projeto Pending Done");

            var taskId = await CreateTaskAsync(
                client,
                projectId,
                "Tarefa Pending Done");

            // Act
            var response = await client.PatchAsync(
                $"/tarefas/{taskId}",
                JsonContent.Create(new
                {
                    status = "done"
                }));

            // Assert
            Assert.Equal((HttpStatusCode)422, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/tarefas/{id}",
                OperationType.Patch,
                422);

            var body = await ReadJsonAsync(response);

            Assert.Equal(
                "invalid_task_status_transition",
                body.GetProperty("code").GetString());
        }

        /// <summary>
        /// Verifica se a tentativa de retroceder uma tarefa de in_progress
        /// para pending retorna 422 Unprocessable Entity e o código
        /// invalid_task_status_transition.
        /// </summary>
        [Fact]
        public async Task PatchTask_InProgressToPending_Returns422_WithCode()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = await CreateProjectAsync(
                client,
                "Projeto Status Retrocesso");

            var taskId = await CreateTaskAsync(
                client,
                projectId,
                "Tarefa Status Retrocesso");

            var startResponse = await client.PatchAsync(
                $"/tarefas/{taskId}",
                JsonContent.Create(new
                {
                    status = "in_progress"
                }));

            startResponse.EnsureSuccessStatusCode();

            // Act
            var response = await client.PatchAsync(
                $"/tarefas/{taskId}",
                JsonContent.Create(new
                {
                    status = "pending"
                }));

            // Assert
            Assert.Equal((HttpStatusCode)422, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/tarefas/{id}",
                OperationType.Patch,
                422);

            var body = await ReadJsonAsync(response);

            Assert.Equal(
                "invalid_task_status_transition",
                body.GetProperty("code").GetString());
        }

        /// <summary>
        /// Verifica se a exclusão de uma tarefa com status pending retorna
        /// 204 No Content e uma resposta aderente ao contrato OpenAPI.
        /// </summary>
        [Fact]
        public async Task DeleteTask_Pending_Returns204()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = await CreateProjectAsync(
                client,
                "Projeto Delete");

            var taskId = await CreateTaskAsync(
                client,
                projectId,
                "Tarefa Delete");

            // Act
            var response = await client.DeleteAsync(
                $"/tarefas/{taskId}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/tarefas/{id}",
                OperationType.Delete,
                204);
        }

        /// <summary>
        /// Verifica se a tentativa de excluir uma tarefa com status in_progress
        /// retorna 422 Unprocessable Entity e o código task_cannot_be_deleted.
        /// </summary>
        [Fact]
        public async Task DeleteTask_InProgress_Returns422_WithCode()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = await CreateProjectAsync(
                client,
                "Projeto Delete In Progress");

            var taskId = await CreateTaskAsync(
                client,
                projectId,
                "Tarefa Não Excluível");

            var startResponse = await client.PatchAsync(
                $"/tarefas/{taskId}",
                JsonContent.Create(new
                {
                    status = "in_progress"
                }));

            startResponse.EnsureSuccessStatusCode();

            // Act
            var response = await client.DeleteAsync(
                $"/tarefas/{taskId}");

            // Assert
            Assert.Equal((HttpStatusCode)422, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/tarefas/{id}",
                OperationType.Delete,
                422);

            var body = await ReadJsonAsync(response);

            Assert.Equal(
                "task_cannot_be_deleted",
                body.GetProperty("code").GetString());
        }

        /// <summary>
        /// Verifica se a tentativa de excluir uma tarefa com status done
        /// retorna 422 Unprocessable Entity e o código task_cannot_be_deleted.
        /// </summary>
        [Fact]
        public async Task DeleteTask_Done_Returns422_WithCode()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var projectId = await CreateProjectAsync(
                client,
                "Projeto Delete Done");

            var taskId = await CreateTaskAsync(
                client,
                projectId,
                "Tarefa Concluída Não Excluível");

            await MoveTaskToDoneAsync(client, taskId);

            // Act
            var response = await client.DeleteAsync(
                $"/tarefas/{taskId}");

            // Assert
            Assert.Equal((HttpStatusCode)422, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/tarefas/{id}",
                OperationType.Delete,
                422);

            var body = await ReadJsonAsync(response);

            Assert.Equal(
                "task_cannot_be_deleted",
                body.GetProperty("code").GetString());
        }

        /// <summary>
        /// Verifica se a tentativa de excluir uma tarefa inexistente retorna
        /// 404 Not Found e o código task_not_found.
        /// </summary>
        [Fact]
        public async Task DeleteTask_NonExisting_Returns404_WithCode()
        {
            // Arrange
            using var factory = CreateFactory();
            using var client = factory.CreateClient();

            var taskId = Guid.NewGuid();

            // Act
            var response = await client.DeleteAsync(
                $"/tarefas/{taskId:D}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            await _validator.ValidateAsync(
                response,
                "/tarefas/{id}",
                OperationType.Delete,
                404);

            var body = await ReadJsonAsync(response);

            Assert.Equal(
                "task_not_found",
                body.GetProperty("code").GetString());
        }

        private static async Task<string> CreateProjectAsync(
            HttpClient client,
            string projectName)
        {
            var response = await client.PostAsJsonAsync(
                "/projetos",
                new
                {
                    name = projectName
                });

            response.EnsureSuccessStatusCode();

            var projectId = (await ReadJsonAsync(response))
                .GetProperty("id")
                .GetString();

            Assert.NotNull(projectId);

            return projectId;
        }

        private static async Task<string> CreateTaskAsync(
            HttpClient client,
            string projectId,
            string taskTitle,
            string priority = "medium")
        {
            var response = await client.PostAsJsonAsync(
                $"/projetos/{projectId}/tarefas",
                new
                {
                    title = taskTitle,
                    priority
                });

            response.EnsureSuccessStatusCode();

            var taskId = (await ReadJsonAsync(response))
                .GetProperty("id")
                .GetString();

            Assert.NotNull(taskId);

            return taskId;
        }

        private static async Task MoveTaskToDoneAsync(
            HttpClient client,
            string taskId)
        {
            var startResponse = await client.PatchAsync(
                $"/tarefas/{taskId}",
                JsonContent.Create(new
                {
                    status = "in_progress"
                }));

            startResponse.EnsureSuccessStatusCode();

            var doneResponse = await client.PatchAsync(
                $"/tarefas/{taskId}",
                JsonContent.Create(new
                {
                    status = "done"
                }));

            doneResponse.EnsureSuccessStatusCode();
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