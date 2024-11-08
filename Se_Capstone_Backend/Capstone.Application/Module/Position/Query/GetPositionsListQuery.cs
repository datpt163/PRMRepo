using MediatR;
using Capstone.Application.Module.Positions.Response;
using Capstone.Application.Common.Paging;

namespace Capstone.Application.Module.Positions.Query
{
    public class GetPositionsListQuery : PagingQuery, IRequest<PagingResultSP<PositionDto>>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
