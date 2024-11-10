namespace Capstone.Api.Module.Statuses.Requests
{
    public class UpdateStatusDefaultRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public bool IsDone { get; set; }
    }
}
