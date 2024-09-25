using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Projects.Command;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.CommandHandle
{
    public class AddMembersToProjectCommandHandle : IRequestHandler<AddMembersToProject, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddMembersToProjectCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMediator> Handle(AddMembersToProject request, CancellationToken cancellationToken)
        {
            var project = _unitOfWork.Projects.Find(x => x.Id == request.ProjectId).Include(c => c.Staffs).FirstOrDefault();
            if (project == null)
                return new ResponseMediator("Project not found", null, 404);
            if(request.MemberIds.Count() == 0)
                return new ResponseMediator("List member empty", null, 400);

            project.Staffs = new List<Staff>();
            foreach (var s in request.MemberIds)
            {
                var staff = _unitOfWork.Staffs.FindOne(x => x.Id == s);
                if (staff == null)
                    return new ResponseMediator("Member not found", null, 404);
                if(staff.Id != project.LeadId)
                    project.Staffs.Add(staff);
            }
            _unitOfWork.Projects.Update(project);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseMediator("", null);
        }
    }
}
