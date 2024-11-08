using Capstone.Application.Module.Positions.Command;
using Capstone.Application.Module.Positions.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Capstone.Application.Module.Positions.CommandHandle
{
    public class AddMultiplePositionsToUserHandler : IRequestHandler<AddMultiplePositionsToUserCommand, AddMultiplePositionsResponse>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Position> _positionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddMultiplePositionsToUserHandler(IRepository<User> userRepository, IRepository<Position> positionRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _positionRepository = positionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddMultiplePositionsResponse> Handle(AddMultiplePositionsToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetQuery()
                .Include(u => u.Positions)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken) ?? throw new Exception("User not found.");
            var existingPositions = new List<string>();
            var notFoundPositions = new List<Guid>();

            foreach (var positionId in request.PositionIds)
            {
                var position = await _positionRepository.GetQueryNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == positionId, cancellationToken);

                if (position == null)
                {
                    notFoundPositions.Add(positionId);
                    continue;
                }

                user.Positions ??= new List<Position>();

                if (user.Positions.Any(s => s.Id == position.Id))
                {
                    existingPositions.Add(position.Title);
                    continue;
                }

                user.Positions.Add(position);
            }

            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var message = string.Empty;

            if (existingPositions.Any())
            {
                message += $"Positions already exist for user: {string.Join(", ", existingPositions)}. ";
            }

            if (notFoundPositions.Any())
            {
                message += $"Positions not found: {string.Join(", ", notFoundPositions)}.";
            }

            return new AddMultiplePositionsResponse(true, message);
        }
    }

}
