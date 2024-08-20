using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Users.Query
{
    public class GetProfileQuery : IRequest<ResponseMediator>
    {
        public string Token { get; set; } = string.Empty;
    }
}
