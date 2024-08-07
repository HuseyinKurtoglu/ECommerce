using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.DataAcces.Absract
{
    // Sipariş verileri ile ilgili CRUD işlemlerini tanımlayan arayüz.
    public interface IOrderRepository
    {
        // Belirli bir siparişi ID'sine göre asenkron olarak getirir.
        Task<Order> GetOrderByIdAsync(int orderId);

        // Tüm siparişleri asenkron olarak getirir.
        Task<IEnumerable<Order>> GetAllOrdersAsync();

        // Yeni bir siparişi asenkron olarak ekler.
        Task AddOrderAsync(Order order);

        // Var olan bir siparişi asenkron olarak günceller.
        Task UpdateOrderAsync(Order order);

        // Belirli bir siparişi ID'sine göre asenkron olarak siler.
        Task DeleteOrderAsync(int orderId);
    }
}
