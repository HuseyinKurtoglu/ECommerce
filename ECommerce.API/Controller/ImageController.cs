using ECommerce.Business.Absract;
using ECommerce.DataAcces.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> AddImage([FromBody] Image image)
        {
            var result = await _imageService.AddImageAsync(image);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateImage([FromBody] Image image)
        {
            var result = await _imageService.UpdateImageAsync(image);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id, int deletedBy)
        {
            var result = await _imageService.DeleteImageAsync(id, deletedBy);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImageById(int id)
        {
            var result = await _imageService.GetImageByIdAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var result = await _imageService.GetAllImagesAsync();
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
