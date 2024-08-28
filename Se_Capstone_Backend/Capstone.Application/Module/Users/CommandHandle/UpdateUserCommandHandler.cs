using Capstone.Application.Common.Cloudinaries;
using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Users.Command;
using Capstone.Application.Module.Users.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Users.CommandHandle
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly CloudinaryService _cloudinaryService;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserCommandHandler(IRepository<Role> roleRepository, IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor, IRepository<User> userRepository, CloudinaryService cloudinaryService, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _cloudinaryService = cloudinaryService;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
        }

        public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User();
            if (request.Id == null)
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext != null)
                {
                    string token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                    user = await _jwtService.VerifyTokenAsync(token);
                }
            }
            else
            {
                user = await _userRepository.GetQuery()
                            .Include(u => u.Roles)
                            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            }


            if (user == null)
            {
                return null; 
            }

            if (!string.IsNullOrEmpty(request.FullName))
            {
                user.FullName = request.FullName;
            }

            if (!string.IsNullOrEmpty(request.Phone))
            {
                user.PhoneNumber = request.Phone;
            }

            if (!string.IsNullOrEmpty(request.Address))
            {
                user.Address = request.Address;
            }

            if (request.Gender.HasValue)
            {
                user.Gender = (Domain.Enums.Gender)request.Gender.Value;
            }
            if (request.Status.HasValue)
            {
                user.Status = (Domain.Enums.UserStatus)request.Status.Value;
            }
            if (request.Dob.HasValue)
            {
                user.Dob = request.Dob;
            }

            if (!string.IsNullOrEmpty(request.BankAccount))
            {
                user.BankAccount = request.BankAccount;
            }

            if (!string.IsNullOrEmpty(request.BankAccountName))
            {
                user.BankAccountName = request.BankAccountName;
            }

            if (request.RoleId.HasValue)
            {
                var role =  _roleRepository.GetQuery().FirstOrDefault(x=> x.Id == request.RoleId.Value);
                if (role !=null)
                {
                    user.Roles.Clear();
                    user.Roles.Add(role);
                }
                
            }

            user.UpdateDate = DateTime.Now;
            if (request.AvatarFile != null)
            {
                using var stream = request.AvatarFile.OpenReadStream();
                var avatarUrl = await _cloudinaryService.UploadImageAsync(stream, request.AvatarFile.FileName);
                user.Avatar = avatarUrl; 
            }
            else
            {
                user.Avatar = request.Avatar; 
            }

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return new UserDto
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
                RoleId = user.Roles.Select(r => r.Id.ToString()).FirstOrDefault(),
                RoleName = user.Roles.Select(r => r.Name).FirstOrDefault()
            };
        }
    }
}
