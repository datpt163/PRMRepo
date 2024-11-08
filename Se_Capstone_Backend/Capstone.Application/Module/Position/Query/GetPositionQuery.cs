using MediatR;
using Capstone.Application.Module.Positions.Response;

namespace Capstone.Application.Module.Positions.Query
{
    public class GetPositionQuery : IRequest<PositionDto?>
    {
        public Guid Id { get; set; }
    }
}
