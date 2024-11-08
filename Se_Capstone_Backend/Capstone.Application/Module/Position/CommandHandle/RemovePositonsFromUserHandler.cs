using Capstone.Application.Module.Positions.Command;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Capstone.Application.Module.Positions.CommandHandle
{
    public class RemovePositonsFromUserHandler : IRequestHandler<RemovePositionsFromUserCommand, bool>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemovePositonsFromUserHandler(IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemovePositionsFromUserCommand request, CancellationToken cancellationToken)
        {
            //var user = await _userRepository.GetQuery()
            //    .Include(x => x.Positions)
            //    .FirstOrDefaultAsync(x => x.Id == request.UserId);

            //if (user == null)
            //{
            //    return false;
            //}

            //var positionsToRemove = user.Positions
            //    .Where(s => request.PositionIds.Contains(s.Id))
            //    .ToList();

            //if (!positionsToRemove.Any())
            //{
            //    return false;
            //}

            //foreach (var position in positionsToRemove)
            //{
            //    user.Positions.Remove(position);
            //}

            //await _userRepository.UpdateAsync(user);
            //await _unitOfWork.SaveChangesAsync();

            return true;
        }


    }

}
