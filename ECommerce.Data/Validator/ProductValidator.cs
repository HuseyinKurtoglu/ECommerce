using ECommerce.DataAcces.Models;
using FluentValidation;

// Ürün doğrulama kurallarını belirleyen sınıf
public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        // Ürün adının boş olmaması gerektiğini belirler
        RuleFor(p => p.ProductName)
            .NotEmpty().WithMessage("Ürün adı gereklidir.");

        // Açıklamanın boş olmaması gerektiğini belirler
        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Açıklama gereklidir.");

        // Fiyatın sıfırdan büyük olması gerektiğini belirler
        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Fiyat sıfırdan büyük olmalıdır.");

        // Stok miktarının negatif olmaması gerektiğini belirler
        RuleFor(p => p.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı negatif olamaz.");

        // Kategori ID'nin sağlanmışsa sıfırdan büyük olması gerektiğini belirler
        RuleFor(p => p.CategoryId)
            .GreaterThan(0).When(p => p.CategoryId.HasValue).WithMessage("Kategori ID sıfırdan büyük olmalıdır eğer sağlanmışsa.");
    }
}
