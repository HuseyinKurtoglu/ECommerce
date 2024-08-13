using ECommerce.DataAcces.Models;
using FluentValidation;

namespace ECommerce.Business.ValidationRules.FluentValidation
{
    public class ImageValidator : AbstractValidator<Image>
    {
        public ImageValidator()
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Resim URL'si boş olamaz.")
                .MaximumLength(500).WithMessage("Resim URL'si en fazla 500 karakter olabilir.");

            RuleFor(x => x.ProductId)
                .NotNull().WithMessage("Ürün ID'si boş olamaz.");
        }
    }
}
