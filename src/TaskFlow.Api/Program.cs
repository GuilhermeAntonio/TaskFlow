using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.Errors;
using TaskFlow.Api.Services.Projects;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options =>
    {
        options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes =
            true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy =
            JsonNamingPolicy.CamelCase;

        options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;

        options.JsonSerializerOptions.DefaultIgnoreCondition =
            JsonIgnoreCondition.Never;

        options.JsonSerializerOptions.UnmappedMemberHandling =
            JsonUnmappedMemberHandling.Disallow;

        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(
                JsonNamingPolicy.SnakeCaseLower,
                allowIntegerValues: false));
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var problemDetails = new ValidationProblemDetails(
                context.ModelState)
            {
                Type = "about:blank",
                Title = "Dados de entrada inválidos",
                Status = StatusCodes.Status400BadRequest,
                Detail = "A requisição contém dados inválidos.",
                Instance = context.HttpContext.Request.Path
            };

            problemDetails.Extensions["code"] = "validation_error";

            return new BadRequestObjectResult(problemDetails)
            {
                ContentTypes = { "application/problem+json" }
            };
        };
    });

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' was not configured.");

builder.Services.AddDbContext<TaskFlowDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<
    TaskFlow.Api.Services.Tasks.ITaskService,
    TaskFlow.Api.Services.Tasks.TaskService>();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    var openApiFilePath = Path.Combine(
        AppContext.BaseDirectory,
        "openapi.yaml");

    app.MapGet(
            "/openapi.yaml",
            () => Results.File(
                openApiFilePath,
                "application/yaml"))
        .ExcludeFromDescription();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi.yaml",
            "TaskFlow — Contrato oficial");

        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public partial class Program { }
