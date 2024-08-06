using Capstone.Application.Module.Shopee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Api.Module.Shopee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {            
        }


        [HttpGet("GetAuth")]
        public string GetUrl()
        {
            return ShopAuth.GetLoginLink();
        }
        [HttpGet("AddItem")]
        public async Task<string> AddItem()
        {
            return await ShopAuth.AddItemAsync();
        }
        [HttpGet("GetToken")]
        public async Task<string?> GetToken(string code, long shopId)
        {
            return await ShopAuth.GetAccessTokenAsync(code, shopId);
        }

    }
}
