using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Phase.Query
{
    public class GetListPhaseQuery : IRequest<ResponseMediator>
    {
        public Guid ProjectId { get; set; }
    }
}
