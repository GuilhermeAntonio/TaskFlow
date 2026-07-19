namespace TaskFlow.Api.Errors.Exceptions
{
    public abstract class ApiException : Exception
    {
        protected ApiException(
            int statusCode,
            string title,
            string code,
            string message)
            : base(message)
        {
            StatusCode = statusCode;
            Title = title;
            Code = code;
        }

        public int StatusCode { get; }

        public string Title { get; }

        public string Code { get; }
    }
}
