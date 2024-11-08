using Capstone.Application.Module.Positions.Command;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Capstone.Application.Module.Positions.CommandHandle
{
    public class AddPositionToUserHandler : IRequestHandler<AddPositionToUserCommand, bool>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Position> _positionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddPositionToUserHandler(IRepository<User> userRepository, IRepository<Position> positionRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _positionRepository = positionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddPositionToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetQuery()
                .Include(u => u.Positions)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                return false;
            }

            var position = await _positionRepository.GetQueryNoTracking().FirstOrDefaultAsync(x=> x.Id == request.PositionId, cancellationToken);
            if (position == null)
            {
                return false;
            }
            user.Positions ??= new List<Position>();

            if (user.Positions.Any(s => s.Id == position.Id))
            {
                return false;
            }

            user.Positions.Add(position);

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }


}
