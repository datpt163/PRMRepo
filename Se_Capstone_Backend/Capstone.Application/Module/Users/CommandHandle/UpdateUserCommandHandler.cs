using Capstone.Application.Common.Cloudinaries;
using Capstone.Application.Module.Users.Command;
using Capstone.Application.Module.Users.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Users.CommandHandle
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IRepository<User> _userRepository;
        private readonly CloudinaryService _cloudinaryService;

        public UpdateUserCommandHandler(IRepository<User> userRepository, CloudinaryService cloudinaryService)
        {
            _userRepository = userRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetQuery().FirstOrDefault(x=> x.Id == request.Id);
            if (user == null)
            {
                return null; 
            }

            user.FullName = request.FullName;
            user.PhoneNumber = request.Phone ?? string.Empty;
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
            };
        }
    }
}
