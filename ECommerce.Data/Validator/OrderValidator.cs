using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ECommerce.DataAcces.Models;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(order => order.CustomerId).NotEmpty().WithMessage("CustomerID is required.");
        RuleFor(order => order.OrderDate).NotEmpty().WithMessage("OrderDate is required.");
        RuleFor(order => order.ShipperId).NotEmpty().WithMessage("ShipperID is required.");
        RuleFor(order => order.StatusId).NotEmpty().WithMessage("StatusID is required.");
        RuleFor(order => order.TotalAmount).GreaterThan(0).WithMessage("TotalAmount must be greater than zero.");
        RuleForEach(order => order.OrderDetails).SetValidator(new OrderDetailValidator());
    }
}

public class OrderDetailValidator : AbstractValidator<OrderDetail>
{
    public OrderDetailValidator()
    {
        RuleFor(detail => detail.ProductId).NotEmpty().WithMessage("ProductID is required.");
        RuleFor(detail => detail.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        RuleFor(detail => detail.UnitPrice).GreaterThan(0).WithMessage("UnitPrice must be greater than zero.");
    }
}

