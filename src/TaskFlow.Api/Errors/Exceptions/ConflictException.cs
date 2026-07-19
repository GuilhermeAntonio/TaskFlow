using Microsoft.AspNetCore.Http;

namespace TaskFlow.Api.Errors.Exceptions
{
    public sealed class ConflictException : ApiException
    {
        public ConflictException(
            string message,
            string code,
            string title = "Conflito de recurso")
            : base(
                StatusCodes.Status409Conflict,
                title,
                code,
                message)
        {
        }
    }
}
