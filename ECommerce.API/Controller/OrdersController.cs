using Microsoft.AspNetCore.Mvc;
using ECommerce.Entities;
using System.Threading.Tasks;
using ECommerce.Business.Absract;
using ECommerce.Business;
using ECommerce.DataAcces.Models;

namespace ECommerce.API.Controller
{
    [ApiController] // Bu sınıfın bir API controller olduğunu belirtir
    [Route("api/[controller]")] // API endpoint'lerinin kök yolunu tanımlar. `[controller]` kısmı, bu controller'ın ismini alır (OrderController için "api/order" olacak)
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService; // Siparişleri yönetmek için kullanılan iş hizmeti

        // Constructor, IOrderService bağımlılığını alır ve private alana atar
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService; // Dependency injection ile service atanır
        }

        // Belirli bir siparişi ID ile alır
        [HttpGet("{id}")] // GET isteği ile ve ID parametresi ile çalışır
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderByIdAsync(id); // Sipariş servisi çağrılır ve sonuç alınır
            return StatusCode((int)result.StatusCode, result); // HTTP yanıt kodu ve sonuç döner
        }

        // Tüm siparişleri alır
        [HttpGet] // GET isteği ile çalışır
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync(); // Tüm siparişler servisten alınır
            return StatusCode((int)result.StatusCode, result); // HTTP yanıt kodu ve sonuç döner
        }

        // Yeni bir sipariş ekler
        [HttpPost] // POST isteği ile çalışır ve request body'den veri alır
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            var result = await _orderService.AddOrderAsync(order); // Sipariş servisi çağrılır ve sonuç alınır
            return StatusCode((int)result.StatusCode, result); // HTTP yanıt kodu ve sonuç döner
        }

        // Var olan bir siparişi günceller
        [HttpPut] // PUT isteği ile çalışır ve request body'den veri alır
        public async Task<IActionResult> UpdateOrder([FromBody] Order order)
        {
            var result = await _orderService.UpdateOrderAsync(order); // Sipariş servisi çağrılır ve sonuç alınır
            return StatusCode((int)result.StatusCode, result); // HTTP yanıt kodu ve sonuç döner
        }

        // Belirli bir siparişi siler
        [HttpDelete("{id}")] // DELETE isteği ile ve ID parametresi ile çalışır
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id); // Sipariş servisi çağrılır ve sonuç alınır
            return StatusCode((int)result.StatusCode, result); // HTTP yanıt kodu ve sonuç döner
        }
    }
}
