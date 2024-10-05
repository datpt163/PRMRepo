using Capstone.Application.Module.Jobs.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Capstone.Application.Module.Jobs.Command
{
    public class CreateJobCommand : IRequest<JobDto>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

