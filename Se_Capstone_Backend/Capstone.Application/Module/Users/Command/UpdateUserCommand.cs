using Capstone.Application.Module.Users.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Users.Command
{
    public class UpdateUserCommand : IRequest<UserDto?>
    {
        public Guid? UserId { get; set; }
        public string? FullName { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Avatar { get; set; } = string.Empty;
        public IFormFile? AvatarFile { get; set; }

    }
}
