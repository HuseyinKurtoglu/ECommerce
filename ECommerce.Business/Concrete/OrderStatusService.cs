using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;

namespace ECommerce.Business.Concrete
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IOrderStatusRepository _orderStatusRepository;

        public OrderStatusService(IOrderStatusRepository orderStatusRepository)
        {
            _orderStatusRepository = orderStatusRepository;
        }

        public async Task<ServiceResult<IEnumerable<OrderStatus>>> GetAllOrderStatusesAsync()
        {
            try
            {
                var result = await _orderStatusRepository.GetAllOrderStatusesAsync();
                return ServiceResult<IEnumerable<OrderStatus>>.SuccessResult(result, "Tüm sipariş durumları başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<OrderStatus>>.FailureResult($"Sipariş durumları getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<OrderStatus>> GetOrderStatusByIdAsync(int statusId)
        {
            try
            {
                var result = await _orderStatusRepository.GetOrderStatusByIdAsync(statusId);
                if (result == null)
                    return ServiceResult<OrderStatus>.FailureResult("Sipariş durumu bulunamadı.");

                return ServiceResult<OrderStatus>.SuccessResult(result, "Sipariş durumu başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                return ServiceResult<OrderStatus>.FailureResult($"Sipariş durumu getirilirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<int>> AddOrderStatusAsync(OrderStatus orderStatus)
        {
            try
            {
                var result = await _orderStatusRepository.AddOrderStatusAsync(orderStatus);
                return ServiceResult<int>.SuccessResult(result, "Sipariş durumu başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Sipariş durumu eklenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<int>> UpdateOrderStatusAsync(OrderStatus orderStatus)
        {
            try
            {
                var result = await _orderStatusRepository.UpdateOrderStatusAsync(orderStatus);
                return ServiceResult<int>.SuccessResult(result, "Sipariş durumu başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Sipariş durumu güncellenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<ServiceResult<int>> DeleteOrderStatusAsync(int statusId, int deletedBy)
        {
            try
            {
                var result = await _orderStatusRepository.DeleteOrderStatusAsync(statusId, deletedBy);
                return ServiceResult<int>.SuccessResult(result, "Sipariş durumu başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Sipariş durumu silinirken hata oluştu: {ex.Message}");
            }
        }
    }
}
