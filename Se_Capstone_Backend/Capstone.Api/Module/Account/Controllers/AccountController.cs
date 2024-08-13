using Capstone.Api.Common.ResponseApi.Controllers;
using Capstone.Api.Common.ResponseApi.Model;
using Capstone.Api.Module.Account.Request;
using Capstone.Application.Module.Account.Command;
using Capstone.Application.Module.Account.Query;
using Capstone.Application.Module.Account.Response;
using Capstone.Application.Module.Profile.Command;
using Capstone.Application.Module.Profile.Query;
using Capstone.Application.Module.Profile.Response;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Capstone.Api.Module.Account.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [SwaggerResponse(200, "Successful", typeof(ResponseSuccess<LoginResponse>))]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [HttpPost("auth")]
        public async Task<IActionResult> Auth([FromBody]LoginQuery loginQuery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _mediator.Send(loginQuery);

            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(dataResponse: result.Data);
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }

        [HttpPost("change-password")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePassRequest request)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _mediator.Send(new ChangePasswordCommand() { Token = token, NewPassword = request.NewPassword, OldPassword = request.OldPassword});
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseNoContent();
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }

        [HttpPost("forgot-password")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [SwaggerResponse(204, "Success")]
        public async Task<IActionResult> SendEmailForgotPass([FromBody] ForgotPasswordQuery forgotPasswordQuery)
        {
            var result = await _mediator.Send(forgotPasswordQuery);
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseNoContent();
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }

        [HttpPost("reset-password")]
        [SwaggerResponse(204, "Success")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand resetPasswordCommand)
        {
            var result = await _mediator.Send(resetPasswordCommand);
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseNoContent();
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }
        [HttpPost("register")]
        [SwaggerResponse(204, "Success")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        public async Task<IActionResult> Register([FromBody] RegisterQuery registerCommand)
        {
            var result = await _mediator.Send(registerCommand);
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseNoContent();
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }

        [HttpPost("verify-otp-register")]
        [SwaggerResponse(204, "Success")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        public async Task<IActionResult> VerifyOtpRegister([FromBody] VerifyOtpRegisterQuery verifyOtpRegisterQuery)
        {
            var result = await _mediator.Send(verifyOtpRegisterQuery);
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseNoContent();
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }

        [HttpPost("test-list-user")]
        [SwaggerResponse(204, "Success")]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        public async Task<IActionResult> Getall()
        {
            return Ok(MyDbContext.Users);
        }

        [SwaggerResponse(200, "Successful", typeof(ResponseSuccess<UpdateUserResponse>))]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserRequest updateUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var updateProfileCommand = new UpdateProfileCommand(token, updateUserRequest.FullName, updateUserRequest.Phone, updateUserRequest.Avatar);
            var result = await _mediator.Send(updateProfileCommand);
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(dataResponse: result.Data);
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }

        [SwaggerResponse(200, "Successful", typeof(ResponseSuccess<UpdateUserResponse>))]
        [SwaggerResponse(400, "Fail", typeof(ResponseFail))]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _mediator.Send(new GetProfileQuery() { Token = token});
            if (string.IsNullOrEmpty(result.ErrorMessage))
                return ResponseOk(dataResponse: result.Data);
            return ResponseBadRequest(messageResponse: result.ErrorMessage);
        }
    }
}
