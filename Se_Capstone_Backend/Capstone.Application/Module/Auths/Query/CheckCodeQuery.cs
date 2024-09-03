using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Auths.Query
{
    public class CheckCodeQuery : IRequest<ResponseMediator>
    {
        public string Code { get; set; } = string.Empty;
    }
}
