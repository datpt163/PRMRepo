using Capstone.Application.Module.Projects.Response;
using MediatR;

namespace Capstone.Application.Module.Projects.Query
{
    public class GetProjectDetailsQuery : IRequest<ProjectEffortCalculationResponse>
    {
        public Guid ProjectId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
