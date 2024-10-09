using Capstone.Application.Common.ResponseMediator;
using Capstone.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Labels.Command
{
    public class CreateLabelCommand : IRequest<ResponseMediator>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public Guid ProjectId { get; set; }
    }
}
