using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.DataAcces.Models;

namespace ECommerce.DataAcces.Absract
{
    public interface IOrderStatusRepository
    {
        Task<IEnumerable<OrderStatus>> GetAllOrderStatusesAsync();
        Task<OrderStatus> GetOrderStatusByIdAsync(int statusId);
        Task<int> AddOrderStatusAsync(OrderStatus orderStatus);
        Task<int> UpdateOrderStatusAsync(OrderStatus orderStatus);
        Task<int> DeleteOrderStatusAsync(int statusId, int deletedBy);
    }
}

