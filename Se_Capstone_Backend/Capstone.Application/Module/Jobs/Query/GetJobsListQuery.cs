using MediatR;
using Capstone.Application.Module.Jobs.Response;
using Capstone.Application.Common.Paging;

namespace Capstone.Application.Module.Jobs.Query
{
    public class GetJobsListQuery : PagingQuery, IRequest<PagingResultSP<JobDto>>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
