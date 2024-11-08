using Capstone.Application.Module.Positions.Command;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Capstone.Application.Module.Positions.CommandHandle
{
    public class RemovePositionFromUserHandler : IRequestHandler<RemovePositionFromUserCommand, bool>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemovePositionFromUserHandler(IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemovePositionFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetQuery()
                .Include(x => x.Positions)
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                return false;
            }

            if (user.Positions == null || !user.Positions.Any())
            {
                return false;
            }

            var position = user.Positions.FirstOrDefault(s => s.Id == request.PositionId);

            if (position == null)
            {
                return false;
            }

            user.Positions.Remove(position);

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

    }

}
