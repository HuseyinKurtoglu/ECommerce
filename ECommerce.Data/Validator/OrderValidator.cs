using FluentValidation;
using ECommerce.DataAcces.Models;

// Siparişleri doğrulamak için kullanılan validator sınıfı.
public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        // Müşteri ID'sinin boş olmamasını doğrular.
        RuleFor(order => order.CustomerId)
            .NotEmpty()
            .WithMessage("CustomerID gereklidir.");

        // Sipariş tarihinin boş olmamasını doğrular.
        RuleFor(order => order.OrderDate)
            .NotEmpty()
            .WithMessage("OrderDate gereklidir.");

        // Gönderici ID'sinin boş olmamasını doğrular.
        RuleFor(order => order.ShipperId)
            .NotEmpty()
            .WithMessage("ShipperID gereklidir.");

        // Durum ID'sinin boş olmamasını doğrular.
        RuleFor(order => order.StatusId)
            .NotEmpty()
            .WithMessage("StatusID gereklidir.");

        // Toplam tutarın sıfırdan büyük olmasını doğrular.
        RuleFor(order => order.TotalAmount)
            .GreaterThan(0)
            .WithMessage("TotalAmount sıfırdan büyük olmalıdır.");

        // Sipariş detaylarının doğrulanması için OrderDetailValidator kullanır.
        RuleForEach(order => order.OrderDetails)
            .SetValidator(new OrderDetailValidator());
    }
}

// Sipariş detaylarını doğrulamak için kullanılan validator sınıfı.
public class OrderDetailValidator : AbstractValidator<OrderDetail>
{
    public OrderDetailValidator()
    {
        // Ürün ID'sinin boş olmamasını doğrular.
        RuleFor(detail => detail.ProductId)
            .NotEmpty()
            .WithMessage("ProductID gereklidir.");

        // Miktarın sıfırdan büyük olmasını doğrular.
        RuleFor(detail => detail.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity sıfırdan büyük olmalıdır.");

        // Birim fiyatının sıfırdan büyük olmasını doğrular.
        RuleFor(detail => detail.UnitPrice)
            .GreaterThan(0)
            .WithMessage("UnitPrice sıfırdan büyük olmalıdır.");
    }
}
