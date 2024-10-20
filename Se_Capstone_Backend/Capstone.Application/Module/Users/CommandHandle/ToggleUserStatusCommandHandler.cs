using Capstone.Application.Module.Users.Command;
using Capstone.Domain.Entities;
using Capstone.Domain.Enums;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Users.CommandHandle
{
    public class ToggleUserStatusCommandHandler : IRequestHandler<ToggleUserStatusCommand, bool>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ToggleUserStatusCommandHandler(IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ToggleUserStatusCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetQuery().FirstOrDefaultAsync(x=> x.Id == command.UserId);

            if (user == null)
            {
                return false; 
            }

            if(user.Status == UserStatus.Inacitve)
            {
                user.Status = UserStatus.Active;
            }
            else
            {
                user.Status = UserStatus.Inacitve;

            }
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return true; 
        }
    }

}
