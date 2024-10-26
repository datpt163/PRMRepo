namespace Capstone.Api.Module.Phases.Request
{
    public class UpdatePhaseRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime ExpectedStartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
    }
}
