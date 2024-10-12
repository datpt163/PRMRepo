using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Status.Query
{
    public class GetListStatusQuery : IRequest<ResponseMediator>
    {
        public Guid? projectId { get; set; }
    }
}
