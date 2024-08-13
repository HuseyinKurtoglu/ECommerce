using ECommerce.Business.Absract;
using ECommerce.DataAcces.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // Tüm ürünleri almak için GET isteği
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProductsAsync();
            // Başarılı ise ürünleri döndür, aksi halde hata mesajı döndür
            return StatusCode((int)result.StatusCode, result);
        }

        // Belirli bir ürünü ID ile almak için GET isteği
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);
         
            return StatusCode((int)result.StatusCode, result);
        }

        // Yeni bir ürün eklemek için POST isteği
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            var result = await _productService.AddProductAsync(product);
            
            return StatusCode((int)result.StatusCode, result);
        }

        // Belirli bir ürünü güncellemek için PUT isteği
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            // Güncellenmek istenen ürünün ID'si ile URL'deki ID'nin eşleşip eşleşmediğini kontrol et
            if (id != product.ProductId) return BadRequest("ID does not match.");
            var result = await _productService.UpdateProductAsync(product);
         
            return StatusCode((int)result.StatusCode, result);
        }

        // Belirli bir ürünü silmek için DELETE isteği
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id, [FromQuery] int deletedBy)
        {
            var result = await _productService.DeleteProductAsync(id, deletedBy);
          
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
