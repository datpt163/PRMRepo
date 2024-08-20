using Capstone.Application.Module.Users.Query;
using Capstone.Application.Module.Users.Response;
using Capstone.Infrastructure.DbContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Users.QueryHandle
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDto>
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
