using Capstone.Application.Module.Shopee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Api.Module.Shopee.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {            
        }


        [HttpGet("get-auth")]
        public string GetUrl()
        {
            return ShopAuth.GetLoginLink();
        }
        [HttpGet("add-item")]
        public async Task<string> AddItem()
        {
            return await ShopAuth.AddItemAsync();
        }
        [HttpGet("get-token")]
        public async Task<string?> GetToken(string code, long shopId)
        {
            return await ShopAuth.GetAccessTokenAsync(code, shopId);
        }

    }
}
