using Capstone.Application.Common.Cloudinaries;
using Capstone.Application.Common.Jwt;
using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Users.Command;
using Capstone.Application.Module.Users.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Users.CommandHandle
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IRepository<User> _userRepository;
        private readonly CloudinaryService _cloudinaryService;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateUserCommandHandler(IHttpContextAccessor httpContextAccessor, IRepository<User> userRepository, CloudinaryService cloudinaryService, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _cloudinaryService = cloudinaryService;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;

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
                 user = _userRepository.GetQuery().FirstOrDefault(x => x.Id == request.Id);

            }


            if (user == null)
            {
                return null; 
            }

            user.FullName = request.FullName;
            user.PhoneNumber = request.Phone ?? string.Empty;
            user.Address = request.Address;
            user.Gender = (Domain.Enums.Gender)request.Gender;
            user.Dob = request.Dob;
            user.BankAccount = request.BankAccount;
            user.BankAccountName = request.BankAccountName;
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

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Phone = user.PhoneNumber,
                Avatar = user.Avatar,
                Address = user.Address,
                Gender = (int)user.Gender,
                Dob = user.Dob,
                BankAccount = user.BankAccount,
                BankAccountName = user.BankAccountName
            };
        }
    }
}
