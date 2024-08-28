using Capstone.Application.Common.Paging;
using Capstone.Application.Module.Users.Query;
using Capstone.Application.Module.Users.Response;
using Capstone.Domain.Entities;
using Capstone.Domain.Enums;
using Capstone.Domain.Helpers;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Users.QueryHandle
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, PagingResultSP<UsersDto>>
    {
        private readonly IRepository<User> _userRepository;

        public GetUserListQueryHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PagingResultSP<UsersDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            // Start building the query with filters
            var usersQuery = _userRepository.GetQueryNoTracking().Include(user => user.Roles).Where(x => x.Status == UserStatus.Active);

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


            int skip = (request.PageIndex - 1) * request.PageSize;
            var pagedUsersQuery = usersQuery.Skip(skip).Take(request.PageSize);

            var userList = await pagedUsersQuery
                .Select(user => new UsersDto
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    FullName = user.FullName,
                    Phone = user.PhoneNumber ?? string.Empty,
                    Avatar = user.Avatar ?? string.Empty,
                    Address = user.Address,
                    Gender = (int)user.Gender,
                    Status =(int)user.Status,
                    Dob = user.Dob ?? DateTime.MinValue,
                    BankAccount = user.BankAccount,
                    BankAccountName = user.BankAccountName,
                    CreateDate = user.CreateDate,
                    UpdateDate = user.UpdateDate,
                    DeleteDate = user.DeleteDate,
                    RoleId = user.Roles.Select(r => r.Id.ToString()).FirstOrDefault(),
                    RoleName = user.Roles.Select(r => r.Name).FirstOrDefault()
                })
                .ToListAsync(cancellationToken);

            int totalCount = await usersQuery.CountAsync(cancellationToken);

            // Create PagingResultSP
            var result = new PagingResultSP<UsersDto>(userList, totalCount, request.PageIndex, request.PageSize);

            return result;
        }
    }
}
