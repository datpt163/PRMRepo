namespace Capstone.Api.Module.Labels.Requests
{
    public class UpdateLabelRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
