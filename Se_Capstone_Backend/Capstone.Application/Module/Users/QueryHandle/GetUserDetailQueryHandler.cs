using Capstone.Application.Module.Users.Query;
using Capstone.Application.Module.Users.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Application.Module.Users.QueryHandle
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDto?>
    {
        private readonly IRepository<User> _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserDetailQueryHandler(IRepository<User> userRepository, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;   
        }

        public async Task<UserDto?> Handle(GetUserDetailQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetQueryNoTracking()
                .Where(u => u.Id == query.UserId).Include(c => c.Position)
                .FirstOrDefaultAsync(cancellationToken);
            
            if (user == null) 
                return null;
            else
            {
                var roles = await _userManager.GetRolesAsync(user);
                string roleId = "";
                string roleName = "";
                if (roles != null && roles.Count > 0)
                {
                    var role = await _unitOfWork.Roles.FindOneAsync(x => x.Name.Equals(roles.FirstOrDefault()));
                    roleId = role.Id + "";
                    roleName = role.Name ?? "";
                }


                return new UserDto()
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    FullName = user.FullName,
                    Phone = user.PhoneNumber ?? string.Empty,
                    Avatar = user.Avatar ?? string.Empty,
                    Address = user.Address,
                    Gender = (int)user.Gender,
                    Status = (int)user.Status,
                    Dob = user.Dob,
                    BankAccount = user.BankAccount,
                    BankAccountName = user.BankAccountName,
                    CreateDate = user.CreateDate,
                    UpdateDate = user.UpdateDate,
                    RoleId = roleId,
                    RoleName = roleName,
                    UserName = user.UserName,
                    PositionName = user.Position != null ? user.Position.Name : ""
                };
            }
        }
    }
}
