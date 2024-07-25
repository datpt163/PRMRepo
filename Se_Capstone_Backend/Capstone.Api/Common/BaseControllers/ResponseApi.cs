namespace Capstone.Api.Common.BaseControllers
{
    public class ResponseApi
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
