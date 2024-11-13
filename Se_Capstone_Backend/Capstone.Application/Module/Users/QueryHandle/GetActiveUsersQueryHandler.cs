using Capstone.Application.Module.Users.Query;
using Capstone.Application.Module.Users.Response;
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

namespace Capstone.Application.Module.Users.QueryHandle
{
    public class GetActiveUsersQueryHandler : IRequestHandler<GetActiveUsersQuery, List<UserStatisticsResponse>>
    {
        private readonly IRepository<User> _userRepository;

        public GetActiveUsersQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserStatisticsResponse>> Handle(GetActiveUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetQueryNoTracking()
                .Include(u => u.Skills)
                .Include(u => u.UserProjects)
                .ThenInclude(c => c.Project)
                .Where(u => u.Status == UserStatus.Active)
                .ToListAsync(cancellationToken);

            var userResponses = users.Select(user => new UserStatisticsResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Skills = string.Join(", ", user.Skills?.Select(s => s.Title) ?? Enumerable.Empty<string>()),
                ActiveProjectCount = user.UserProjects.Select(x => x.Project).Count(p => p.Status == ProjectStatus.Finished)
            }).ToList();

            return userResponses;
        }
    }
}
