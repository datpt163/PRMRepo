using Capstone.Application.Common.Cloudinaries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Api.Module.Extension.Controllers
{
    [Route("api/extension")]
    [ApiController]
    public class ExtensionController : ControllerBase
    {
        private readonly CloudinaryService _cloudinaryService;

        public ExtensionController(CloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost("upload-image")]
        [AllowAnonymous]

        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            using var stream = image.OpenReadStream();
            var url = await _cloudinaryService.UploadImageAsync(stream, image.FileName);
            return Ok(new { Url = url });
        }


        [HttpDelete("delete-image")]
        [AllowAnonymous]

        public async Task<IActionResult> DeleteImage([FromQuery] string imageUrl)
        {
            var success = await _cloudinaryService.DeleteImageByUrlAsync(imageUrl);
            if (success)
            {
                return NoContent();
            }
            return NotFound("Image not found or could not be deleted.");
        }
    }

}
