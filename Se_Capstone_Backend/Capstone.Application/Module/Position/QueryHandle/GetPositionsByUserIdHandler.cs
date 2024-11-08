using Capstone.Application.Module.Positions.Query;
using Capstone.Application.Module.Positions.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Application.Module.Positions.QueryHandle
{
    public class GetPositionsByUserIdHandler : IRequestHandler<GetPositionsByUserIdQuery, List<PositionDto>>
    {
        private readonly IRepository<User> _userRepository;

        public GetPositionsByUserIdHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<PositionDto>> Handle(GetPositionsByUserIdQuery request, CancellationToken cancellationToken)
        {
            //var user = await _userRepository.GetQueryNoTracking()
            //                .Include(x => x.Positions)
            //                .FirstOrDefaultAsync(x => x.Id == request.UserId);

            //if (user?.Positions != null)
            //{
            //    user.Positions = user.Positions.Where(position => !position.IsDeleted).ToList();
            //}


            //if (user == null || user.Positions == null)
            //{
            //    return new List<PositionDto>();
            //}

            //var positionDtos = user.Positions.Select(position => new PositionDto
            //{
            //    Id = position.Id,
            //    Title = position.Title,
            //    Description = position.Description
            //}).ToList();

            //return positionDtos;
            throw new NotImplementedException();
        }
    }

}
