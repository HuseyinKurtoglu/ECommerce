using System;
using System.Collections.Generic;
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

    public OrderService(IOrderRepository orderRepository, IValidator<Order> orderValidator, IValidator<OrderDetail> orderDetailValidator)
    {
        _orderRepository = orderRepository;
        _orderValidator = orderValidator;
        _orderDetailValidator = orderDetailValidator;
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
        await ValidateAndExecuteAsync(order, () => _orderRepository.AddOrderAsync(order));
    }

    public async Task UpdateOrderAsync(Order order)
    {
        await ValidateAndExecuteAsync(order, () => _orderRepository.UpdateOrderAsync(order));
    }

    public async Task DeleteOrderAsync(int orderId)
    {
        await ExecuteAsync(() => _orderRepository.DeleteOrderAsync(orderId));
    }

    private async Task ValidateAndExecuteAsync(Order order, Func<Task> action)
    {
        ValidationResult result = await _orderValidator.ValidateAsync(order);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }

        foreach (var detail in order.OrderDetails)
        {
            ValidationResult detailResult = await _orderDetailValidator.ValidateAsync(detail);
            if (!detailResult.IsValid)
            {
                throw new ValidationException(detailResult.Errors);
            }
        }

        await ExecuteAsync(action);
    }

    private async Task ExecuteAsync(Func<Task> action)
    {
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while processing your request.", ex);
        }
    }
}
