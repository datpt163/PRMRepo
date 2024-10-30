using Capstone.Application.Common.ResponseMediator;
using Capstone.Domain.Entities;
using Capstone.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Issues.Command
{
    public class AddIssueCommand : IRequest<ResponseMediator>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Priority? Priority { get; set; }
        public int? EstimatedTime { get; set; } 
        public Guid? ParentIssueId { get; set; }
        public Guid AssignedId { get; set; }
        public Guid? LastUpdateById { get; set; }
        public Guid ProjectId { get; set; }
        public Guid StatusId { get; set; }
        public Guid? LabelId { get; set; }

    }
}
