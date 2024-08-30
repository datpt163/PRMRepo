using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Auths.Query
{
    public class ForgotPasswordQuery : IRequest<ResponseMediator>
    {
        public string? Email { get; set; }
    }
}
