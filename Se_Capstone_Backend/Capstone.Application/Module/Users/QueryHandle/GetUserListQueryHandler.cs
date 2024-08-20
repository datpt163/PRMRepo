using Capstone.Application.Module.Users.Query;
using Capstone.Application.Module.Users.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Users.QueryHandle
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, List<UserDto>>
    {
        private readonly IRepository<User> _userRepository;

        public GetUserListQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var usersQuery = _userRepository.GetQueryNoTracking();

            if (!string.IsNullOrEmpty(request.FullName))
            {
                usersQuery = usersQuery.Where(user => user.FullName.Contains(request.FullName));
            }

            if (!string.IsNullOrEmpty(request.Phone))
            {
                usersQuery = usersQuery.Where(user => user.PhoneNumber.Contains(request.Phone));
            }

            return await usersQuery
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
