using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Api.Module.Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet("Auth")]
        public async Task<IActionResult> AuthAsync()
        {
            //(string accessToken, string errorMessage) = await AccountService.AuthAsync(accountAuthRequest);
            //if (!string.IsNullOrEmpty(errorMessage))
            //{
            //    return ResponseBadRequest(errorMessage);
            //}
            //return ResponseOk(new { accessToken });
            return Ok(new { });
        }
    }
}
