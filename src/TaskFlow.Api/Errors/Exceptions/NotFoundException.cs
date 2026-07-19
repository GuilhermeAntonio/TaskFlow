using Microsoft.AspNetCore.Http;

namespace TaskFlow.Api.Errors.Exceptions
{
    public sealed class NotFoundException : ApiException
    {
        public NotFoundException(
            string message,
            string code,
            string title = "Recurso não encontrado")
            : base(
                StatusCodes.Status404NotFound,
                title,
                code,
                message)
        {
        }
    }
}
