#nullable disable

using System.Net;
using System.Runtime.Serialization;

namespace Application.Shared.Exceptions
{
    [Serializable]
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message)
        {
            HttpStatusCode = HttpStatusCode.InternalServerError;
            ErrorMessages = new List<string>();
        }

        public ApiException(HttpStatusCode httpStatusCode, string message) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public ApiException(HttpStatusCode httpStatusCode, string message, string detailedMessage) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            ErrorMessages = new List<string> { detailedMessage };
        }

        public ApiException(HttpStatusCode httpStatusCode, string message, IEnumerable<string> errorMessages) : base(message)
        {
            HttpStatusCode = httpStatusCode;
            ErrorMessages = errorMessages;
        }

        protected ApiException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
            HttpStatusCode = HttpStatusCode.InternalServerError;
            ErrorMessages = new List<string>();
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        public IEnumerable<string> ErrorMessages { get; set; }
    }
}