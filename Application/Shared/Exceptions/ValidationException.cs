#nullable disable

using FluentValidation.Results;
using System.Net;
using System.Runtime.Serialization;

namespace Application.Shared.Exceptions
{
    [Serializable]
    public class ValidationException : ApiException
    {
        public ValidationException() : base(HttpStatusCode.BadRequest, "One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(IList<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public IDictionary<string, string[]> Failures { get; }
    }
}