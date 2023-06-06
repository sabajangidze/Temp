#nullable disable

using Newtonsoft.Json;
using System.Net;

namespace Application.Shared
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            HttpStatusCode = HttpStatusCode.OK;
        }

        public BaseResponse(string message)
        {
            HttpStatusCode = HttpStatusCode.InternalServerError;
            Message = message;
        }

        public BaseResponse(Exception ex)
        {
            HttpStatusCode = HttpStatusCode.InternalServerError;
            Message = "A System Error occured, please try again";
            if (ex.InnerException?.Message != null)
            {
                AddError(ex.InnerException.Message);
            }

            if (ex.InnerException?.InnerException?.Message != null)
            {
                AddError(ex.InnerException.InnerException.Message);
            }

            if (ex.Message != null)
            {
                AddError(ex.Message);
            }
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Errors { get; set; }

        public void AddError(string description)
        {
           Errors ??= new List<string>();
           Errors.Add(description);
        }
    }
}