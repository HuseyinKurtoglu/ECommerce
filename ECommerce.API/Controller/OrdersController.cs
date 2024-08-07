using Microsoft.AspNetCore.Mvc;
using ECommerce.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using ECommerce.Business.Absract;
using ECommerce.DataAcces.Models;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    // Constructor, OrderService'i dependency injection yoluyla alır.
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // Belirli bir siparişi ID'sine göre alır.
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var result = await _orderService.GetOrderByIdAsync(id);

        // İşlem başarılıysa 200 OK, başarısızsa 400 Bad Request döner.
        return result.Success ? Ok(result.Message) : BadRequest(result.Message);
    }

    // Tüm siparişleri alır.
    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var result = await _orderService.GetAllOrdersAsync();

        // İşlem başarılıysa 200 OK, başarısızsa 400 Bad Request döner.
        return result.Success ? Ok(result.Message) : BadRequest(result.Message);
    }

    // Yeni bir sipariş ekler.
    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody] Order order)
    {
        var result = await _orderService.AddOrderAsync(order);

        // İşlem başarılıysa 200 OK, başarısızsa 400 Bad Request döner.
        return result.Success ? Ok(result.Message) : BadRequest(result.Message);
    }

    // Var olan bir siparişi günceller.
    [HttpPut]
    public async Task<IActionResult> UpdateOrder([FromBody] Order order)
    {
        var result = await _orderService.UpdateOrderAsync(order);

        // İşlem başarılıysa 200 OK, başarısızsa 400 Bad Request döner.
        return result.Success ? Ok(result.Message) : BadRequest(result.Message);
    }

    // Belirli bir siparişi ID'sine göre siler.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var result = await _orderService.DeleteOrderAsync(id);

        // İşlem başarılıysa 200 OK, başarısızsa 400 Bad Request döner.
        return result.Success ? Ok(result.Message) : BadRequest(result.Message);
    }
}
