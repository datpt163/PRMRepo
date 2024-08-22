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
using Capstone.Domain.Helpers;
using Capstone.Domain.Enums;

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
            var usersQuery = _userRepository.GetQueryNoTracking().Where(x => x.Status == StatusUser.Active);

            if (!string.IsNullOrEmpty(request.FullName))
            {
                usersQuery = usersQuery.Where(user => user.FullName.Contains(request.FullName));
            }

            if (!string.IsNullOrEmpty(request.Phone))
            {
                if (!PhoneNumberValidator.Validate(request.Phone))
                {
                    throw new ArgumentException("Invalid phone number format.");
                }

                usersQuery = usersQuery.Where(user => user.PhoneNumber.Contains(request.Phone));
            }

            if (!string.IsNullOrEmpty(request.Address))
            {
                usersQuery = usersQuery.Where(user => user.Address.Contains(request.Address));
            }

            if (request.Gender.HasValue)
            {
                usersQuery = usersQuery.Where(user => (int)user.Gender == request.Gender.Value);
            }

            if (request.DobFrom.HasValue)
            {
                usersQuery = usersQuery.Where(user => user.Dob >= request.DobFrom.Value);
            }

            if (request.DobTo.HasValue)
            {
                usersQuery = usersQuery.Where(user => user.Dob <= request.DobTo.Value);
            }

            return await usersQuery
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    FullName = user.FullName,
                    Phone = user.PhoneNumber ?? string.Empty,
                    Avatar = user.Avatar ?? string.Empty,
                    Address = user.Address,
                    Gender = (int)user.Gender, 
                    Dob = user.Dob ?? DateTime.MinValue, 
                    BankAccount = user.BankAccount,
                    BankAccountName = user.BankAccountName,
                })
                .ToListAsync(cancellationToken);
        }
    }
}
