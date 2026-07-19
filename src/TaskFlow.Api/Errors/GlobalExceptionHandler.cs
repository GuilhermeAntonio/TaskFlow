using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskFlow.Api.Errors.Exceptions;

namespace TaskFlow.Api.Errors
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var problemDetails = exception switch
            {
                ValidationException validationException =>
                    CreateValidationProblemDetails(
                        httpContext,
                        validationException),

                ApiException apiException =>
                    CreateProblemDetails(
                        httpContext,
                        apiException),

                _ =>
                    CreateUnexpectedProblemDetails(httpContext)
            };

            if (exception is not ValidationException &&
                exception is not ApiException)
            {
                _logger.LogError(
                    exception,
                    "Ocorreu uma exceção não tratada durante a requisição {Method} {Path}.",
                    httpContext.Request.Method,
                    httpContext.Request.Path);
            }

            httpContext.Response.StatusCode =
                problemDetails.Status ??
                StatusCodes.Status500InternalServerError;

            var problemDetailsService =
                httpContext.RequestServices
                    .GetRequiredService<IProblemDetailsService>();

            await problemDetailsService.WriteAsync(
                new ProblemDetailsContext
                {
                    HttpContext = httpContext,
                    ProblemDetails = problemDetails,
                    Exception = exception
                });

            return true;
        }

        private static ValidationProblemDetails
            CreateValidationProblemDetails(
                HttpContext httpContext,
                ValidationException exception)
        {
            var problemDetails =
                new ValidationProblemDetails(exception.Errors)
                {
                    Type = "about:blank",
                    Title = "Dados de entrada inválidos",
                    Status = StatusCodes.Status400BadRequest,
                    Detail = exception.Message,
                    Instance = httpContext.Request.Path
                };

            problemDetails.Extensions["code"] =
                "validation_error";

            return problemDetails;
        }

        private static ProblemDetails CreateProblemDetails(
            HttpContext httpContext,
            ApiException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "about:blank",
                Title = exception.Title,
                Status = exception.StatusCode,
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions["code"] =
                exception.Code;

            return problemDetails;
        }

        private static ProblemDetails
            CreateUnexpectedProblemDetails(
                HttpContext httpContext)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "about:blank",
                Title = "Erro interno do servidor",
                Status =
                    StatusCodes.Status500InternalServerError,
                Detail = "Ocorreu um erro inesperado.",
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions["code"] =
                "unexpected_error";

            return problemDetails;
        }
    }
}
