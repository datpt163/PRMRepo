

namespace Capstone.Application.Module.Users.Response
{
    public class UserStatisticsResponse
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public int ActiveProjectCount { get; set; }
    }
}
