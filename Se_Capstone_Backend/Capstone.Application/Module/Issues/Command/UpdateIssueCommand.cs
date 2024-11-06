using Capstone.Application.Common.ResponseMediator;
using Capstone.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Issues.Command
{
    public class UpdateIssueCommand : IRequest<ResponseMediator>
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int Percentage { get; set; }
        public Priority? Priority { get; set; }
        public float? EstimatedTime { get; set; }
        public Guid? ParentIssueId { get; set; }
        public Guid? AssigneeId { get; set; }
        public Guid StatusId { get; set; }
        public Guid? LabelId { get; set; }
        public Guid? PhaseId { get; set; }
    }
}
