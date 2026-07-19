using System.Collections.Generic;

namespace TaskFlow.Api.Errors.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public ValidationException(string message, IDictionary<string, string[]>? errors = null)
            : base(message)
        {
            Errors = errors ?? new Dictionary<string, string[]>();
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
