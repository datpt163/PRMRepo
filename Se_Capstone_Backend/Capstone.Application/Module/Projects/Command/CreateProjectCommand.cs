using Capstone.Application.Common.ResponseMediator;
using Capstone.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.Command
{
    public class CreateProjectCommand : IRequest<ResponseMediator>
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [DefaultValue("2024-10-21T09:50:31.798")]
        public DateTime StartDate { get; set; }
        [DefaultValue("2024-10-22T09:50:31.798")]
        public DateTime EndDate { get; set; }
        public bool? IsVisivle { get; set; }
        public Guid? LeadId { get; set; }
    }
}
