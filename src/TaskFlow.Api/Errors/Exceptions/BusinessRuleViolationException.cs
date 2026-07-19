using Microsoft.AspNetCore.Http;

namespace TaskFlow.Api.Errors.Exceptions
{
    public sealed class BusinessRuleViolationException : ApiException
    {
        public BusinessRuleViolationException(
            string message,
            string code,
            string title = "Regra de negócio violada")
            : base(
                StatusCodes.Status422UnprocessableEntity,
                title,
                code,
                message)
        {
        }
    }
}
