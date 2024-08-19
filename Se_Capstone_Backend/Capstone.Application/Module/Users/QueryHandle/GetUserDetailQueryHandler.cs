using Capstone.Application.Module.Users.Models;
using Capstone.Application.Module.Users.Query;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Users.QueryHandle
{
    public class GetUserDetailQueryHandler
    {
        private readonly SeCapstoneContext _context;

        public GetUserDetailQueryHandler(SeCapstoneContext context)
        {
            _context = context;
        }

        public async Task<UserDto?> Handle(GetUserDetailQuery query, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(u => u.Id == query.UserId)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Email = u.Email ?? string.Empty,
                    FullName = u.FullName,
                    Phone = u.PhoneNumber ?? string.Empty,
                    Avatar = u.Avatar
                })
                .FirstOrDefaultAsync(cancellationToken);

            return user;
        }
    }
}
