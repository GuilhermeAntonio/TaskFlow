namespace TaskFlow.Api.Errors.Exceptions
{
    public sealed class BusinessRuleViolationException : Exception
    {
        public BusinessRuleViolationException(string message) : base(message)
        {
        }
    }
}
