using ECommerce.Business.Absract;
using ECommerce.DataAcces.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderStatusController : ControllerBase
    {
        // Sipariş durumlarıyla ilgili işlemleri yapacak servis
        private readonly IOrderStatusService _orderStatusService;

        // OrderStatusController sınıfının yapıcı metodu, bağımlılık enjeksiyonunu sağlar
        public OrderStatusController(IOrderStatusService orderStatusService)
        {
            // Verilen servis örneğini sınıf değişkenine atar
            _orderStatusService = orderStatusService;
        }

        // Tüm sipariş durumlarını getirir
        [HttpGet]
        public async Task<IActionResult> GetAllOrderStatuses()
        {
            // Servisi kullanarak tüm sipariş durumlarını alır
            var result = await _orderStatusService.GetAllOrderStatusesAsync();

            // Sonucu HTTP yanıtı olarak döner
            return StatusCode((int)result.StatusCode, result);
        }

        // Belirli bir sipariş durumunu getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderStatusById(int id)
        {
            // Servisi kullanarak belirli bir sipariş durumunu ID'ye göre alır
            var result = await _orderStatusService.GetOrderStatusByIdAsync(id);

            // Sonucu HTTP yanıtı olarak döner
            return StatusCode((int)result.StatusCode, result);
        }

        // Yeni bir sipariş durumu ekler
        [HttpPost]
        public async Task<IActionResult> AddOrderStatus(OrderStatus orderStatus)
        {
            // Servisi kullanarak yeni bir sipariş durumu ekler
            var result = await _orderStatusService.AddOrderStatusAsync(orderStatus);

            // Sonucu HTTP yanıtı olarak döner
            return StatusCode((int)result.StatusCode, result);
        }

        // Mevcut bir sipariş durumunu günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatus orderStatus)
        {
            // Güncellenmek istenen sipariş durumunun ID'sini alır ve orderStatus nesnesine atar
            orderStatus.StatusId = id;

            // Servisi kullanarak sipariş durumunu günceller
            var result = await _orderStatusService.UpdateOrderStatusAsync(orderStatus);

            // Sonucu HTTP yanıtı olarak döner
            return StatusCode((int)result.StatusCode, result);
        }

        // Bir sipariş durumunu siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderStatus(int id, [FromQuery] int deletedBy)
        {
            // Servisi kullanarak sipariş durumunu siler
            var result = await _orderStatusService.DeleteOrderStatusAsync(id, deletedBy);

            // Sonucu HTTP yanıtı olarak döner
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
