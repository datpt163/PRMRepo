namespace Capstone.Api.Common.BaseControllers
{
    public class BaseResponse
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

        public BaseResponse(object data, string message, int statusCode)
        {
            Data = data;
            Message = message;
            StatusCode = statusCode;
        }

        // Constructor for success response without data
        public BaseResponse(string message, int statusCode = 200)
        {
            Message = message;
            StatusCode = statusCode;
        }

        // Constructor for error response
        public BaseResponse(int statusCode, string message)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }

    public class ResponseSuccess : BaseResponse
    {
        public ResponseSuccess(object data, string message = "Operation successful.") : base(data, message, 200)
        {
        }
    }

    // Bad request response
    public class ResponseBadRequest : BaseResponse
    {
        public ResponseBadRequest(string message = "Bad request.") : base(null, message, 400)
        {
        }
    }

    // Not found response
    public class ResponseNotFound : BaseResponse
    {
        public ResponseNotFound(string message = "Resource not found.") : base(null, message, 404)
        {
        }
    }

    // Internal server error response
    public class ResponseInternalServerError : BaseResponse
    {
        public ResponseInternalServerError(string message = "An unexpected error occurred.") : base(null, message, 500)
        {
        }
    }

    // Unauthorized response
    public class ResponseUnauthorized : BaseResponse
    {
        public ResponseUnauthorized(string message = "Unauthorized access.") : base(null, message, 401)
        {
        }
    }
}
