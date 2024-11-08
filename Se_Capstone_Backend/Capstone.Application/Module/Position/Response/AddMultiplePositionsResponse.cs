

namespace Capstone.Application.Module.Positions.Response
{
    public class AddMultiplePositionsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public AddMultiplePositionsResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
