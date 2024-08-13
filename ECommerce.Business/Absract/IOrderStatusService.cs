using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.DataAcces.Models;

namespace ECommerce.Business.Absract
{
    public interface IOrderStatusService
    {
        Task<ServiceResult<IEnumerable<OrderStatus>>> GetAllOrderStatusesAsync();
        Task<ServiceResult<OrderStatus>> GetOrderStatusByIdAsync(int statusId);
        Task<ServiceResult<int>> AddOrderStatusAsync(OrderStatus orderStatus);
        Task<ServiceResult<int>> UpdateOrderStatusAsync(OrderStatus orderStatus);
        Task<ServiceResult<int>> DeleteOrderStatusAsync(int statusId, int deletedBy);
    }
}
