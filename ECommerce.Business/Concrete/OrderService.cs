using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            try
            {
                await _orderRepository.AddOrderAsync(order);
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error adding order", ex);
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            try
            {
                await _orderRepository.UpdateOrderAsync(order);
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error updating order", ex);
            }
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            try
            {
                await _orderRepository.DeleteOrderAsync(orderId);
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error deleting order", ex);
            }
        }
    }

}
