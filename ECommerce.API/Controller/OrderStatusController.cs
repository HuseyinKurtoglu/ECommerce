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
        private readonly IOrderStatusService _orderStatusService;

        public OrderStatusController(IOrderStatusService orderStatusService)
        {
            _orderStatusService = orderStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderStatuses()
        {
            var result = await _orderStatusService.GetAllOrderStatusesAsync();
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderStatusById(int id)
        {
            var result = await _orderStatusService.GetOrderStatusByIdAsync(id);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderStatus(OrderStatus orderStatus)
        {
            var result = await _orderStatusService.AddOrderStatusAsync(orderStatus);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderStatus(int id, OrderStatus orderStatus)
        {
            orderStatus.StatusId = id; 
            var result = await _orderStatusService.UpdateOrderStatusAsync(orderStatus);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderStatus(int id, [FromQuery] int deletedBy)
        {
            var result = await _orderStatusService.DeleteOrderStatusAsync(id, deletedBy);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }
    }
}
