using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Position.Queries;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Capstone.Application.Module.Position.QueryHandle
{
    public class GetListPositionQueryHandle : IRequestHandler<GetListPositionQuery, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        public  GetListPositionQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseMediator> Handle(GetListPositionQuery request, CancellationToken cancellationToken)
        {
            return new ResponseMediator("", await _unitOfWork.Positions.GetQuery().ToListAsync());
        }
    }
}
