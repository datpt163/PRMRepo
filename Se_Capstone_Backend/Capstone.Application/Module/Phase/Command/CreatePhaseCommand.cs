using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Phase.Command
{
    public class CreatePhaseCommand : IRequest<ResponseMediator>
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime ExpectedStartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public Guid ProjectId { get; set; }
    }
}
