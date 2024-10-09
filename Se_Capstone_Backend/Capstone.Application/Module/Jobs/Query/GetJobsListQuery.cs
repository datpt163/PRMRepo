using MediatR;
using Capstone.Application.Module.Jobs.Response;

namespace Capstone.Application.Module.Jobs.Query
{
    public class GetJobsListQuery : IRequest<List<JobDto>>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
