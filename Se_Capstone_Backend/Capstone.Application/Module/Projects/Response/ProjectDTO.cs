using Capstone.Domain.Entities;
using Capstone.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.Response
{
    public class ProjectDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ProjectStatus Status { get; set; } = ProjectStatus.NotStarted;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsVisible { get; set; } = false;
        public Guid? LeadId { get; set; }
        public string? LeadName { get; set; } 
    }

    public class UserDTO
    {
        public Guid Id;
        public string Name = string.Empty;
    }
}
