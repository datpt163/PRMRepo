using Capstone.Application.Module.Skills.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Capstone.Application.Module.Skills.Command
{
    public class CreateSkillCommand : IRequest<SkillDto>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

