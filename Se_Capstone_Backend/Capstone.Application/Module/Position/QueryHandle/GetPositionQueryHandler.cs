using Capstone.Domain.Entities;
using Capstone.Application.Module.Positions.Query;
using Capstone.Application.Module.Positions.Response;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Application.Module.Positions.QueryHandle
{
    public class GetPositionQueryHandler : IRequestHandler<GetPositionQuery, PositionDto?>
    {
        private readonly IRepository<Position> _positionRepository;

        public GetPositionQueryHandler(IRepository<Position> positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public async Task<PositionDto?> Handle(GetPositionQuery request, CancellationToken cancellationToken)
        {
            var position =  await _positionRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (position == null)
            {
                return null;
            }

            return new PositionDto
            {
                Id = position.Id,
                Title = position.Title,
                Description = position.Description,
                CreatedAt = position.CreatedAt,
                UpdatedAt = position.UpdatedAt,
                IsDeleted = position.IsDeleted,
                CreatedBy = position.CreatedBy,
                UpdatedBy = position.UpdatedBy
            };
        }
    }
}
