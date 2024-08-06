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

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody] Order order)
    {
        await _orderService.AddOrderAsync(order);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateOrder([FromBody] Order order)
    {
        await _orderService.UpdateOrderAsync(order);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        await _orderService.DeleteOrderAsync(id);
        return Ok();
    }
}
