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

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IValidator<Order> _orderValidator;
    private readonly IValidator<OrderDetail> _orderDetailValidator;

    // Constructor, gerekli bağımlılıkları dependency injection yoluyla alır.
    public OrderService(IOrderRepository orderRepository, IValidator<Order> orderValidator, IValidator<OrderDetail> orderDetailValidator)
    {
        _orderRepository = orderRepository;
        _orderValidator = orderValidator;
        _orderDetailValidator = orderDetailValidator;
    }

    // Belirli bir siparişi ID'sine göre asenkron olarak getirir.
    public async Task<ServiceResult<Order>> GetOrderByIdAsync(int orderId)
    {
        try
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                // Sipariş bulunamadığında hata mesajı döner.
                return ServiceResult<Order>.FailureResult("Sipariş bulunamadı.");
            }
            // Sipariş başarıyla alındığında başarı mesajı döner.
            return ServiceResult<Order>.SuccessResult(order, "Sipariş başarıyla alındı.");
        }
        catch (Exception ex)
        {
            // Hata durumunda hata mesajı döner.
            return ServiceResult<Order>.FailureResult($"Sipariş alınırken hata oluştu: {ex.Message}");
        }
    }

    // Tüm siparişleri asenkron olarak getirir.
    public async Task<ServiceResult<IEnumerable<Order>>> GetAllOrdersAsync()
    {
        try
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            // Tüm siparişler başarıyla alındığında başarı mesajı döner.
            return ServiceResult<IEnumerable<Order>>.SuccessResult(orders, "Tüm siparişler başarıyla alındı.");
        }
        catch (Exception ex)
        {
            // Hata durumunda hata mesajı döner.
            return ServiceResult<IEnumerable<Order>>.FailureResult($"Siparişler alınırken hata oluştu: {ex.Message}");
        }
    }

    // Yeni bir siparişi asenkron olarak ekler.
    public async Task<ServiceResult> AddOrderAsync(Order order)
    {
        return await ValidateAndExecuteAsync(order, () => _orderRepository.AddOrderAsync(order), "Sipariş başarıyla eklendi.");
    }

    // Var olan bir siparişi asenkron olarak günceller.
    public async Task<ServiceResult> UpdateOrderAsync(Order order)
    {
        return await ValidateAndExecuteAsync(order, () => _orderRepository.UpdateOrderAsync(order), "Sipariş başarıyla güncellendi.");
    }

    // Belirli bir siparişi ID'sine göre asenkron olarak siler.
    public async Task<ServiceResult> DeleteOrderAsync(int orderId)
    {
        return await ExecuteAsync(() => _orderRepository.DeleteOrderAsync(orderId), "Sipariş başarıyla silindi.");
    }

    // Siparişi doğrulayıp işlemi gerçekleştiren asenkron bir yardımcı yöntemdir.
    private async Task<ServiceResult> ValidateAndExecuteAsync(Order order, Func<Task> action, string successMessage)
    {
        // Siparişin doğruluğunu kontrol eder.
        ValidationResult result = await _orderValidator.ValidateAsync(order);
        if (!result.IsValid)
        {
            // Doğrulama hataları varsa hata mesajı döner.
            return ServiceResult.FailureResult(string.Join("; ", result.Errors.Select(e => e.ErrorMessage)));
        }

        // Sipariş detaylarını doğrular.
        foreach (var detail in order.OrderDetails)
        {
            ValidationResult detailResult = await _orderDetailValidator.ValidateAsync(detail);
            if (!detailResult.IsValid)
            {
                // Detay doğrulama hataları varsa hata mesajı döner.
                return ServiceResult.FailureResult(string.Join("; ", detailResult.Errors.Select(e => e.ErrorMessage)));
            }
        }

        // Doğrulama başarılıysa işlemi gerçekleştirir.
        return await ExecuteAsync(action, successMessage);
    }

    // Verilen işlemi asenkron olarak gerçekleştirir ve başarı mesajı döner.
    private async Task<ServiceResult> ExecuteAsync(Func<Task> action, string successMessage)
    {
        try
        {
            await action();
            // İşlem başarılıysa başarı mesajı döner.
            return ServiceResult.SuccessResult(successMessage);
        }
        catch (Exception ex)
        {
            // İşlem sırasında hata oluşursa hata mesajı döner.
            return ServiceResult.FailureResult($"İşlem gerçekleştirilirken hata oluştu: {ex.Message}");
        }
    }
}
