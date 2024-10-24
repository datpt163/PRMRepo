using Capstone.Application.Module.Users.Response;
using MediatR;


namespace Capstone.Application.Module.Users.Query
{
    public class GetActiveUsersQuery : IRequest<List<UserStatisticsResponse>>
    {
    }
}
