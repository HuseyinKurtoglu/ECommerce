using FluentValidation;
using ECommerce.DataAcces.Models;

// Ödeme işlemlerini doğrulamak için kullanılan validator sınıfı.
public class PaymentValidator : AbstractValidator<Payment>
{
    public PaymentValidator()
    {
        // Miktarın sıfırdan büyük olmasını doğrular.
        RuleFor(payment => payment.Amount)
            .GreaterThan(0)
            .WithMessage("Tutar sıfırdan büyük olmalıdır.");

        // Ödeme yönteminin boş olmamasını doğrular.
        RuleFor(payment => payment.PaymentMethod)
            .NotEmpty()
            .WithMessage("Ödeme yöntemi belirtilmelidir.");

        // Sipariş ID'sinin boş olmamasını doğrular.
        RuleFor(payment => payment.OrderId)
            .NotEmpty()
            .WithMessage("Sipariş ID'si gereklidir.");

        // Ödeme tarihinin geçerli bir tarih olduğunu doğrular.
        RuleFor(payment => payment.PaymentDate)
            .NotEmpty()
            .WithMessage("Ödeme tarihi gereklidir.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Ödeme tarihi gelecekte olamaz.");

        // Diğer doğrulamalar burada yapılabilir.
    }
}
