using Capstone.Application.Module.Users.Response;
using MediatR;

namespace Capstone.Application.Module.Users.Query
{
    public class GetUserDetailQuery : IRequest<UserDto?>
    {
        public Guid UserId { get; set; }

        public GetUserDetailQuery(Guid userId) 
        {
            UserId = userId;
        }
    }
}