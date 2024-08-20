﻿using Capstone.Application.Module.Users.Query;
using Capstone.Application.Module.Users.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.DbContexts;
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
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDto>
    {
        private readonly SeCapstoneContext _context;
        private readonly IRepository<User> _userRepository;

        public GetUserDetailQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> Handle(GetUserDetailQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetQueryNoTracking()
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
