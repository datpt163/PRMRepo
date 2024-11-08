using Capstone.Domain.Entities;
using Capstone.Application.Module.Positions.Command;
using Capstone.Application.Module.Positions.Response;
using Capstone.Infrastructure.Repository;
using MediatR;

namespace Capstone.Application.Module.Positions.CommandHandle
{
    public class UpdatePositionCommandHandler : IRequestHandler<UpdatePositionCommand, PositionDto?>
    {
        private readonly IRepository<Position> _positionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePositionCommandHandler(IRepository<Position> positionRepository, IUnitOfWork unitOfWork)
        {
            _positionRepository = positionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PositionDto?> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
        {
            var position = _positionRepository.GetQuery().FirstOrDefault(x=> x.Id ==request.Id);
            if (position == null)
            {
                return null;
            }
            if (request.Isdeleted != null)
            position.IsDeleted = (bool)request.Isdeleted;
            if(!string.IsNullOrEmpty(request.Title))
            position.Title = request.Title;
            if(!string.IsNullOrEmpty(request.Description))
            position.Description = request.Description;

            position.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            position.UpdatedBy = null;

            await _positionRepository.UpdateAsync(position);
            await _unitOfWork.SaveChangesAsync();
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
