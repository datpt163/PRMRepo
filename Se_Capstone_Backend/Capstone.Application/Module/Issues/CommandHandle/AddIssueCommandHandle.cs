using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Issues.Command;
using Capstone.Infrastructure.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Issues.CommandHandle
{
    public class AddIssueCommandHandle : IRequestHandler<AddIssueCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddIssueCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(AddIssueCommand request, CancellationToken cancellationToken)
        {
           if(string.IsNullOrEmpty(request.Title))
                return new ResponseMediator("Title empty", null, 400);

           if(request.StartDate.HasValue && request.DueDate.HasValue && request.StartDate.Value.Date > request.DueDate.Value.Date)
                return new ResponseMediator("Start date must greater or equal due date", null, 400);
            return new ResponseMediator("Title empty", null, 400);
        }
    }
}
