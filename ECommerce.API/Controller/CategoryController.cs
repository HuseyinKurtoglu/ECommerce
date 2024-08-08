using ECommerce.Business.Absract;
using ECommerce.DataAcces.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        // Constructor, bağımlılığı enjeksiyon yoluyla alır
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Tüm kategorileri asenkron olarak getirir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Kategorileri veri erişim katmanından alır
            var result = await _categoryService.GetAllCategoriesAsync();
            // Sonucu HTTP durum kodu ile döner
            return StatusCode((int)result.StatusCode, result);
        }

        // Belirli bir kategoriyi ID'ye göre asenkron olarak getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // Belirtilen ID'ye sahip kategoriyi veri erişim katmanından alır
            var result = await _categoryService.GetCategoryByIdAsync(id);
            // Sonucu HTTP durum kodu ile döner
            return StatusCode((int)result.StatusCode, result);
        }

        // Yeni bir kategori oluşturur
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            // Yeni kategoriyi veri erişim katmanına ekler
            var result = await _categoryService.AddCategoryAsync(category);
            // Oluşturulan kategorinin sonucunu HTTP durum kodu ile döner
            return StatusCode((int)result.StatusCode, result);
        }

        // Var olan bir kategoriyi günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            // ID'lerin uyuşup uyuşmadığını kontrol eder
            if (id != category.CategoryId)
            {
                return BadRequest("ID uyuşmazlığı.");
            }
            // Kategoriyi veri erişim katmanında günceller
            var result = await _categoryService.UpdateCategoryAsync(category);
            // Güncellenmiş kategorinin sonucunu HTTP durum kodu ile döner
            return StatusCode((int)result.StatusCode, result);
        }

        // Belirli bir kategoriyi siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromQuery] int deletedBy)
        {
            // Kategoriyi veri erişim katmanında siler
            var result = await _categoryService.DeleteCategoryAsync(id, deletedBy);
            // Silinen kategorinin sonucunu HTTP durum kodu ile döner
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
