using Capstone.Application.Common.ResponseMediator;
using MediatR;
using System.ComponentModel;


namespace Capstone.Application.Module.Auth.Query
{
    public class LoginQuery : IRequest<ResponseMediator>
    {
        [DefaultValue("datpt163@gmail.com")]
        public string Email { get; set; } = string.Empty;
        [DefaultValue("@Dat1234")]
        public string Password { get; set; } = string.Empty;
    }
}
