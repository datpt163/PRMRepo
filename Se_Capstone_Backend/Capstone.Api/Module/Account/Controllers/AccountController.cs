using Capstone.Api.Common.BaseControllers;
using Capstone.Api.Module.Account.Request;
using Capstone.Application.Module.Account.Command;
using Capstone.Application.Module.Account.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Api.Module.Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Auth")]
        public async Task<IActionResult> Auth([FromBody]LoginQuery loginQuery)
        {
            var result = await _mediator.Send(loginQuery);

            if (result.IsSuccess)
                return Ok(new ResponseSuccess(data: result.Data));
            return BadRequest(new ResponseBadRequest( message: result.Message));
        }

        [HttpPatch("Password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePassRequest request)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _mediator.Send(new ChangePasswordCommand() { token = token, NewPassword = request.NewPassword, OldPassword = request.OldPassword});
            if (result.IsSuccess)
                return Ok( new ResponseSuccess(data: null) );
            return BadRequest(new ResponseBadRequest(message: result.Message));
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> SendEmailForgotPass([FromBody] ForgotPasswordQuery forgotPasswordQuery)
        {
            var result = await _mediator.Send(forgotPasswordQuery);
            if (result.IsSuccess)
                return Ok(new ResponseSuccess(data: null));
            return BadRequest(new ResponseBadRequest(message: result.Message));
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand resetPasswordCommand)
        {
            var result = await _mediator.Send(resetPasswordCommand);
            if (result.IsSuccess)
                return Ok(new ResponseSuccess(data: null));
            return BadRequest(new ResponseBadRequest(message: result.Message));
        }
    }
}
