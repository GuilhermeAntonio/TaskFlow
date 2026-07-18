using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Hosting;
using TaskFlow.Api.Errors.Exceptions;

namespace TaskFlow.Api.Errors
{
    public static class GlobalExceptionHandler
    {
        public static void ConfigureExceptionHandler(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var problemDetailsFactory = context.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature?.Error;

                    var problemDetails = exception switch
                    {
                        ValidationException validation => CreateValidationProblemDetails(context, problemDetailsFactory, validation),
                        NotFoundException notFound => CreateProblemDetails(context, problemDetailsFactory, HttpStatusCode.NotFound, "Recurso não encontrado", notFound.Message, "project_not_found"),
                        ConflictException conflict => CreateProblemDetails(context, problemDetailsFactory, HttpStatusCode.Conflict, "Conflito de recurso", conflict.Message, "project_name_conflict"),
                        BusinessRuleViolationException businessRule => CreateProblemDetails(context, problemDetailsFactory, HttpStatusCode.UnprocessableEntity, "Regra de negócio violada", businessRule.Message, "project_has_in_progress_tasks"),
                        _ => CreateProblemDetails(context, problemDetailsFactory, HttpStatusCode.InternalServerError, "Erro interno do servidor", "Ocorreu um erro inesperado.", "unexpected_error")
                    };

                    context.Response.ContentType = "application/problem+json";
                    context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;

                    await context.Response.WriteAsJsonAsync(problemDetails);
                });
            });
        }

        private static ProblemDetails CreateProblemDetails(HttpContext context, ProblemDetailsFactory factory, HttpStatusCode statusCode, string title, string detail, string code)
        {
            var problemDetails = factory.CreateProblemDetails(context, (int)statusCode, title, detail, context.Request.Path);
            problemDetails.Extensions["code"] = code;
            return problemDetails;
        }

        private static ValidationProblemDetails CreateValidationProblemDetails(HttpContext context, ProblemDetailsFactory factory, ValidationException validation)
        {
            var validationProblemDetails = new ValidationProblemDetails(validation.Errors)
            {
                Type = "about:blank",
                Title = "Dados de entrada inválidos",
                Status = StatusCodes.Status400BadRequest,
                Detail = validation.Message,
                Instance = context.Request.Path
            };
            validationProblemDetails.Extensions["code"] = "validation_error";
            return validationProblemDetails;
        }
    }
}
