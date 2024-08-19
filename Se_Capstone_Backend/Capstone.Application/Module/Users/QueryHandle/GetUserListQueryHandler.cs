using Capstone.Application.Module.Users.Models;
using Capstone.Application.Module.Users.Query;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore; 

namespace Capstone.Application.Module.Users.QueryHandle
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<UserDto>>
    {
        private readonly SeCapstoneContext _context;

        public GetUserListQueryHandler(SeCapstoneContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Users
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    FullName = user.FullName,
                    Phone = user.PhoneNumber ?? string.Empty,
                    Avatar = user.Avatar
                })
                .ToListAsync(cancellationToken);
        }
    }
}
