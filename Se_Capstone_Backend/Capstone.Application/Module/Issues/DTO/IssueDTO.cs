﻿using Capstone.Application.Module.Projects.Response;
using Capstone.Domain.Entities;
using Capstone.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Issues.DTO
{
    public class IssueDTO
    {
        public Guid Id { get; set; }
        public int Index { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int Percentage { get; set; } = 0;
        public Priority? Priority { get; set; }
        public int? EstimatedTime { get; set; }
        public int? ActualTime { get; set; } = 0;
        public int PercentDone { get; set; } = 0;
        public int Position { get; set; }
        public Domain.Entities.Phase? Phase { get; set; }
        public Label? Label { get; set; }
        public Domain.Entities.Status? Status { get; set; }
        public UserForProjectDetailDTO? LastUpdateBy { get; set; }
        public UserForProjectDetailDTO Reporter { get; set; } = null!;
        public UserForProjectDetailDTO? Assignee { get; set; } = null!;
        public List<IssueDTO> SubIssues { get; set; } = new List<IssueDTO>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }


}
