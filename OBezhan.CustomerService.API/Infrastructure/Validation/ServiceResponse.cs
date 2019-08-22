using Newtonsoft.Json;
using System.Net;

namespace OBezhan.CustomerService.API.Infrastructure.Validation
{
    public class ServiceResponse
    {
        public ServiceResponse()
        {
            Errors = new ErrorResponse();
        }
        public ServiceResponse(HttpStatusCode statusCode, ErrorResponse errors = null)
        {
            StatusCode = statusCode;
            Errors = errors ?? new ErrorResponse();
        }

        public HttpStatusCode StatusCode { get; set; }
        public ErrorResponse Errors { get; set; }

        [JsonIgnore]
        internal bool IsSuccess => (int)StatusCode >= 200 && (int)StatusCode < 400;
    }

    public class ServiceResponse<TResponse> : ServiceResponse
    {
        public ServiceResponse() { }
        public ServiceResponse(TResponse response, HttpStatusCode statusCode, ErrorResponse errors = null) 
            : base(statusCode, errors)
        {
            Response = response;
        }

        public TResponse Response { get; set; }
    }
}
