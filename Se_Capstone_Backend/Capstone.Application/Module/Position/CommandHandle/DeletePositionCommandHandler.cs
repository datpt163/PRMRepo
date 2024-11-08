using Capstone.Domain.Entities;
using Capstone.Application.Module.Positions.Command;
using Capstone.Infrastructure.Repository;
using MediatR;

namespace Capstone.Application.Module.Positions.CommandHandle
{
    public class DeletePositionCommandHandler : IRequestHandler<DeletePositionCommand, bool>
    {
        private readonly IRepository<Position> _positionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePositionCommandHandler(IRepository<Position> positionRepository, IUnitOfWork unitOfWork)
        {
            _positionRepository = positionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePositionCommand request, CancellationToken cancellationToken)
        {
            var position = _positionRepository.GetQuery().FirstOrDefault(x=> x.Id ==request.Id);
            if (position == null || position.IsDeleted)
            {
                return false;
            }
            position.IsDeleted = true;
            await _positionRepository.UpdateAsync(position);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
