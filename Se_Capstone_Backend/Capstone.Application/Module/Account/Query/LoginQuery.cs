using Capstone.Application.Common.ResponseMediator;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Application.Module.Account.Query
{
    public class LoginQuery : IRequest<ResponseMediator>
    {
        [DefaultValue("datpt163@gmail.com")]
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
    