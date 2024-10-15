using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Labels.Command;
using Capstone.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UglyToad.PdfPig.Graphics.Operations.MarkedContent;

namespace Capstone.Application.Module.Labels.CommandHandle
{
    public class DeleteLabelCommandHandle : IRequestHandler<DeleteLabelCommand, ResponseMediator>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteLabelCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ResponseMediator> Handle(DeleteLabelCommand request, CancellationToken cancellationToken)
        {
            var label = _unitOfWork.Labels.Find(x => x.Id == request.Id).Include(c => c.Issues).Include(c => c.Project).FirstOrDefault();
            if (label == null)
                return new ResponseMediator("Label not found", null, 404);
            if(label.Issues.Count == 0)
            {
                _unitOfWork.Labels.Remove(label);
                await _unitOfWork.SaveChangesAsync();
                return new ResponseMediator("", null);
            }
            else
            {
                if(!request.NewLabelId.HasValue)
                    return new ResponseMediator("New label null", null, 400);
                var newLabel = _unitOfWork.Labels.Find(x => x.Id == request.NewLabelId).FirstOrDefault();
                if(newLabel == null)
                    return new ResponseMediator("new label not found", null, 404);
                if(newLabel.ProjectId != label.ProjectId)
                    return new ResponseMediator("Old label and new label not same project", null, 400);

                foreach(var i in label.Issues)
                    i.LabelId = newLabel.Id;

                _unitOfWork.Labels.Update(label);
                _unitOfWork.Labels.Remove(label);
                await _unitOfWork.SaveChangesAsync();
                return new ResponseMediator("", null);
            }
        }
    }
}
