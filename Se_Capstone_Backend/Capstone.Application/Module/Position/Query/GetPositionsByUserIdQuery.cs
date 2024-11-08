using Capstone.Application.Module.Positions.Response;
using MediatR;

namespace Capstone.Application.Module.Positions.Query
{
    public class GetPositionsByUserIdQuery : IRequest<List<PositionDto>>
    {
        public Guid UserId { get; set; }
    }


}
