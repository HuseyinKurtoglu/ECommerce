using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;

namespace ECommerce.Business.Concrete
{
    public class OrderStatusService : IOrderStatusService
    {
        // Sipariş durumlarıyla işlem yapacak repository
        private readonly IOrderStatusRepository _orderStatusRepository;

        // OrderStatusService sınıfının yapıcı metodu, bağımlılık enjeksiyonunu sağlar
        public OrderStatusService(IOrderStatusRepository orderStatusRepository)
        {
            // Verilen repository örneğini sınıf değişkenine atar
            _orderStatusRepository = orderStatusRepository;
        }

        // Tüm sipariş durumlarını asenkron olarak getirir.
        public async Task<ServiceResult<IEnumerable<OrderStatus>>> GetAllOrderStatusesAsync()
        {
            try
            {
                // Repository'den tüm sipariş durumlarını alır
                var result = await _orderStatusRepository.GetAllOrderStatusesAsync();

                // Başarıyla getirildiğinde ServiceResult ile döner
                // HTTP 200 (OK) durumu ile döner
                return ServiceResult<IEnumerable<OrderStatus>>.SuccessResult(result, "Tüm sipariş durumları başarıyla getirildi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner
                // HTTP 406 (Not Acceptable) durumu ile döner
                return ServiceResult<IEnumerable<OrderStatus>>.FailureResult($"Sipariş durumları getirilirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Belirli bir sipariş durumunu asenkron olarak getirir.
        public async Task<ServiceResult<OrderStatus>> GetOrderStatusByIdAsync(int statusId)
        {
            try
            {
                // Repository'den belirli bir sipariş durumunu alır
                var result = await _orderStatusRepository.GetOrderStatusByIdAsync(statusId);

                if (result == null)
                {
                    // Sipariş durumu bulunamadığında hata mesajı döner
                    // HTTP 404 (Not Found) durumu ile döner
                    return ServiceResult<OrderStatus>.FailureResult("Sipariş durumu bulunamadı.", HttpStatusCode.NotFound);
                }

                // Sipariş durumu başarıyla getirildiğinde ServiceResult ile döner
                // HTTP 200 (OK) durumu ile döner
                return ServiceResult<OrderStatus>.SuccessResult(result, "Sipariş durumu başarıyla getirildi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner
                // HTTP 406 (Not Acceptable) durumu ile döner
                return ServiceResult<OrderStatus>.FailureResult($"Sipariş durumu getirilirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Yeni bir sipariş durumunu asenkron olarak ekler.
        public async Task<ServiceResult<int>> AddOrderStatusAsync(OrderStatus orderStatus)
        {
            try
            {
                // Repository'ye yeni sipariş durumunu ekler ve eklenen sipariş durumunun ID'sini alır
                var result = await _orderStatusRepository.AddOrderStatusAsync(orderStatus);

                // Başarıyla eklendiğinde ServiceResult ile yeni sipariş durumunun ID'si döner
                // HTTP 201 (Created) durumu ile döner
                return ServiceResult<int>.SuccessResult(result, "Sipariş durumu başarıyla eklendi.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner
                // HTTP 406 (Not Acceptable) durumu ile döner
                return ServiceResult<int>.FailureResult($"Sipariş durumu eklenirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Mevcut bir sipariş durumunu asenkron olarak günceller.
        public async Task<ServiceResult<int>> UpdateOrderStatusAsync(OrderStatus orderStatus)
        {
            try
            {
                // Repository'de sipariş durumunu günceller ve güncellenmiş sipariş durumunun ID'sini alır
                var result = await _orderStatusRepository.UpdateOrderStatusAsync(orderStatus);

                // Başarıyla güncellendiğinde ServiceResult ile güncellenmiş sipariş durumunun ID'si döner
                // HTTP 200 (OK) durumu ile döner
                return ServiceResult<int>.SuccessResult(result, "Sipariş durumu başarıyla güncellendi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner
                // HTTP 406 (Not Acceptable) durumu ile döner
                return ServiceResult<int>.FailureResult($"Sipariş durumu güncellenirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Bir sipariş durumunu asenkron olarak siler.
        public async Task<ServiceResult<int>> DeleteOrderStatusAsync(int statusId, int deletedBy)
        {
            try
            {
                // Repository'de sipariş durumunu siler ve silinen sipariş durumunun ID'sini alır
                var result = await _orderStatusRepository.DeleteOrderStatusAsync(statusId, deletedBy);

                // Başarıyla silindiğinde ServiceResult ile silinen sipariş durumunun ID'si döner
                // HTTP 200 (OK) durumu ile döner
                return ServiceResult<int>.SuccessResult(result, "Sipariş durumu başarıyla silindi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner
                // HTTP 406 (Not Acceptable) durumu ile döner
                return ServiceResult<int>.FailureResult($"Sipariş durumu silinirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }
    }
}
