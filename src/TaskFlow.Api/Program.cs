using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.Errors;
using TaskFlow.Api.Filters;
using TaskFlow.Api.Serialization;
using TaskFlow.Api.Services.Projects;
using TaskFlow.Api.Contracts;

var builder = WebApplication.CreateBuilder(args);

var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = false,
    DefaultIgnoreCondition = JsonIgnoreCondition.Never
};
jsonOptions.Converters.Add(new JsonStringEnumConverter(new SnakeCaseNamingPolicy()));
jsonOptions.Converters.Add(new OptionalFieldJsonConverterFactory());

builder.Services.AddControllers(options =>
    {
        options.InputFormatters.Insert(0, new RejectUnknownJsonPropertiesInputFormatter(jsonOptions));
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(new SnakeCaseNamingPolicy()));
        options.JsonSerializerOptions.Converters.Add(new OptionalFieldJsonConverterFactory());
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
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

builder.Services.AddSwaggerGen();

// Register DbContext (SQLite) - connection string from configuration
var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' was not configured.");
builder.Services.AddDbContext<TaskFlowDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IProjectService, ProjectService>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    GlobalExceptionHandler.ConfigureExceptionHandler(errorApp, app.Environment);
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
