using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using ECommerce.Entities;
using FluentValidation;
using FluentValidation.Results;
using System.Net;

namespace ECommerce.Business.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository; // Sipariş verilerini yönetmek için kullanılan repository
        private readonly IValidator<Order> _orderValidator; // Sipariş doğrulama işlemleri için validator
        private readonly IValidator<OrderDetail> _orderDetailValidator; // Sipariş detaylarını doğrulamak için validator

        public OrderService(IOrderRepository orderRepository, IValidator<Order> orderValidator, IValidator<OrderDetail> orderDetailValidator)
        {
            _orderRepository = orderRepository; // Dependency injection ile repository atanır
            _orderValidator = orderValidator; // Dependency injection ile sipariş validator atanır
            _orderDetailValidator = orderDetailValidator; // Dependency injection ile sipariş detay validator atanır
        }

        // Belirli bir siparişi ID ile alır
        public async Task<ServiceResult<Order>> GetOrderByIdAsync(int orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId); // Repository'den sipariş alınır
                if (order == null)
                {
                    return ServiceResult<Order>.FailureResult("Sipariş bulunamadı.", HttpStatusCode.NotFound); // Sipariş bulunamazsa hata mesajı döner
                }
                return ServiceResult<Order>.SuccessResult(order, "Sipariş başarıyla alındı."); // Sipariş bulunursa başarı mesajı döner
            }
            catch (Exception ex)
            {
                return ServiceResult<Order>.FailureResult($"Sipariş alınırken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable); // Hata durumunda mesaj döner
            }
        }

        // Tüm siparişleri alır
        public async Task<ServiceResult<IEnumerable<Order>>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _orderRepository.GetAllOrdersAsync(); // Repository'den tüm siparişler alınır
                return ServiceResult<IEnumerable<Order>>.SuccessResult(orders, "Tüm siparişler başarıyla alındı."); // Başarı mesajı döner
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Order>>.FailureResult($"Siparişler alınırken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable); // Hata durumunda mesaj döner
            }
        }

        // Yeni bir sipariş ekler
        public async Task<ServiceResult> AddOrderAsync(Order order)
        {
            return await ValidateAndExecuteAsync(order, () => _orderRepository.AddOrderAsync(order), "Sipariş başarıyla eklendi.", HttpStatusCode.Created); // Siparişi doğrulayıp ekler
        }

        // Var olan bir siparişi günceller
        public async Task<ServiceResult> UpdateOrderAsync(Order order)
        {
            return await ValidateAndExecuteAsync(order, () => _orderRepository.UpdateOrderAsync(order), "Sipariş başarıyla güncellendi.", HttpStatusCode.OK); // Siparişi doğrulayıp günceller
        }

        // Belirli bir siparişi siler
        public async Task<ServiceResult> DeleteOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId); // Repository'den sipariş alınır
            if (order == null)
            {
                return ServiceResult.FailureResult("Sipariş bulunamadı.", HttpStatusCode.NotFound); // Sipariş bulunamazsa hata mesajı döner
            }

            return await ExecuteAsync(() => _orderRepository.DeleteOrderAsync(orderId), "Sipariş başarıyla silindi.", HttpStatusCode.NoContent); // Siparişi siler
        }

        // Sipariş doğrulamasını yapıp verilen işlemi asenkron olarak çalıştırır
        private async Task<ServiceResult> ValidateAndExecuteAsync(Order order, Func<Task> action, string successMessage, HttpStatusCode successStatusCode)
        {
            ValidationResult result = await _orderValidator.ValidateAsync(order); // Sipariş doğrulaması yapılır
            if (!result.IsValid)
            {
                return ServiceResult.FailureResult(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)), HttpStatusCode.BadRequest); // Geçersizse hata mesajı döner
            }

            foreach (var detail in order.OrderDetails)
            {
                ValidationResult detailResult = await _orderDetailValidator.ValidateAsync(detail); // Sipariş detayları doğrulanır
                if (!detailResult.IsValid)
                {
                    return ServiceResult.FailureResult(string.Join("; ", detailResult.Errors.Select(e => e.ErrorMessage)), HttpStatusCode.BadRequest); // Geçersizse hata mesajı döner
                }
            }

            return await ExecuteAsync(action, successMessage, successStatusCode); // Doğrulama başarılıysa işlem gerçekleştirilir
        }

        // Verilen asenkron işlemi çalıştırır ve başarı mesajı döner
        private async Task<ServiceResult> ExecuteAsync(Func<Task> action, string successMessage, HttpStatusCode successStatusCode)
        {
            try
            {
                await action(); // Asenkron işlemi çalıştırır
                return ServiceResult.SuccessResult(successMessage, successStatusCode); // Başarı mesajı döner
            }
            catch (Exception ex)
            {
                return ServiceResult.FailureResult($"İşlem gerçekleştirilirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable); // Hata durumunda mesaj döner
            }
        }
    }
}
