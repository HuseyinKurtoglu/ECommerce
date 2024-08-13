using ECommerce.DataAcces.Models;
using FluentValidation;

namespace ECommerce.Business.ValidationRules
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            // Kategori adının doğrulama kuralları
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")    // Kategori adı boş olamaz
                .Length(2, 100).WithMessage("Kategori adı 2 ile 100 karakter arasında olmalıdır."); // Kategori adı 2 ile 100 karakter arasında olmalıdır

            // Açıklamanın doğrulama kuralları
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz.")    // Açıklama boş olamaz
                .Length(5, 500).WithMessage("Açıklama 5 ile 500 karakter arasında olmalıdır."); // Açıklama 5 ile 500 karakter arasında olmalıdır
        }
    }
}
