

namespace Capstone.Application.Module.Skills.Response
{
    public class AddMultipleSkillsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public AddMultipleSkillsResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
