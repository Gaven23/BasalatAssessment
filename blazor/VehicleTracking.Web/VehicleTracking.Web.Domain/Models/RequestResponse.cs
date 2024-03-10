using System.Net;

namespace VehicleTracking.Web.Domain.Models
{
    public class RequestResponse<TResult> : RequestResponse
    {
        public RequestResponse()
        {
        }

        public RequestResponse(HttpStatusCode statusCode)
            : base(statusCode)
        {
        }

        public TResult? Result { get; set; }
    }

    public class RequestResponse
    {
        private const string TimeOutResponseErrorCode = "database_timeout";

        public RequestResponse()
        {
        }

        public RequestResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public bool IsSuccessful { get; set; } = true;
        public bool IsCancelled { get; set; }
        public bool IsDatabaseTimeout
        {
            get
            {
                return HasErrorCode(TimeOutResponseErrorCode);
            }
        }
        public bool IsBadRequest
        {
            get
            {
                return StatusCode == HttpStatusCode.BadRequest && !IsDatabaseTimeout;
            }
        }
        public bool IsNotFound
        {
            get
            {
                return StatusCode == HttpStatusCode.NotFound;
            }
        }
        public bool IsUnauthorized
        {
            get
            {
                return StatusCode == HttpStatusCode.Unauthorized;
            }
        }

        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public Exception Exception { get; set; }

        public bool HasErrorCode(string errorCode)
        {
            return ErrorCode != null && ErrorCode.Equals(errorCode, StringComparison.OrdinalIgnoreCase);
        }
    }
}
