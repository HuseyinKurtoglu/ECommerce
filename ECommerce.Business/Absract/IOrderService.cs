using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.DataAcces.Models;
using ECommerce.Entities;

public interface IOrderService
{
    // Belirli bir siparişi ID'sine göre asenkron olarak getirir.
    Task<ServiceResult<Order>> GetOrderByIdAsync(int orderId);

    // Tüm siparişleri asenkron olarak getirir.
    Task<ServiceResult<IEnumerable<Order>>> GetAllOrdersAsync();

    // Yeni bir siparişi asenkron olarak ekler.
    Task<ServiceResult> AddOrderAsync(Order order);

    // Var olan bir siparişi asenkron olarak günceller.
    Task<ServiceResult> UpdateOrderAsync(Order order);

    // Belirli bir siparişi ID'sine göre asenkron olarak siler.
    Task<ServiceResult> DeleteOrderAsync(int orderId);
}
