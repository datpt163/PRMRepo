using Capstone.Application.Common.ResponseMediator;
using Capstone.Application.Module.Projects.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Projects.Query
{
    public class GetProjectDetailsQuery : IRequest<ProjectEffortCalculationResponse>
    {
        public Guid ProjectId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
