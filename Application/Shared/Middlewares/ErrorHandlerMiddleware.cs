#nullable disable

using Application.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Application.Shared.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                var errorTypeName = error.GetType().Name;
                response.ContentType = "application/json";

                switch (errorTypeName)
                {
                    case nameof(ApiException):
                        await HandleApiException(error as ApiException, response);
                        break;
                    case nameof(KeyNotFoundException):
                        await HandleKeyNotFoundException(error as KeyNotFoundException, response);
                        break;
                    case nameof(ValidationException):
                        await HandleValidationException(error as ValidationException, response);
                        break;
                    default:
                        await HandleUnknownException(error, response);
                        break;
                }
            }
        }

        private async Task HandleValidationException(ValidationException exception, HttpResponse response)
        {
            await HandleException(response, HttpStatusCode.BadRequest, exception.Failures.SelectMany(failure => failure.Value));
        }

        private async Task HandleUnknownException(Exception exception, HttpResponse response)
        {
            await HandleException(response, HttpStatusCode.InternalServerError, GetInnerExceptions(exception).Select(e => e.Message));
        }

        private async Task HandleKeyNotFoundException(KeyNotFoundException exception, HttpResponse response)
        {
            await HandleException(response, HttpStatusCode.NotFound, new List<string> { exception.Message });
        }

        private async Task HandleApiException(ApiException exception, HttpResponse response)
        {
            await HandleException(response, HttpStatusCode.BadRequest, exception.ErrorMessages);
        }

        private async Task HandleException(
            HttpResponse response,
            HttpStatusCode httpStatusCode,
            IEnumerable<string> errors,
            string message = "Internal System Error")
        {
            var responseModel = new BaseResponse() { Errors = errors.ToList(), HttpStatusCode = httpStatusCode };
            response.StatusCode = (int)httpStatusCode;

            var result = JsonSerializer.Serialize(responseModel);
            await response.WriteAsync(result);
        }

        public static IEnumerable<Exception> GetInnerExceptions(Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException(nameof(ex));
            }

            var innerException = ex;
            do
            {
                yield return innerException;
                innerException = innerException.InnerException;
            }
            while (innerException != null);
        }
    }
}