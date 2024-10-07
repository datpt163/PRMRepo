using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Position.Queries;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
