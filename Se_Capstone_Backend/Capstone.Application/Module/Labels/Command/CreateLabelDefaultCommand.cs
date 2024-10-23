using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Labels.Command
{
    public class CreateLabelDefaultCommand : IRequest<ResponseMediator>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
