using FluentValidation;
using ECommerce.DataAcces.Models;

public class ProductReviewValidator : AbstractValidator<ProductReview>
{
    public ProductReviewValidator()
    {
        RuleFor(pr => pr.ProductId)
            .NotNull().WithMessage("Ürün ID'si boş olamaz.")
            .GreaterThan(0).WithMessage("Ürün ID'si sıfırdan büyük olmalıdır.");

        RuleFor(pr => pr.CustomerId)
            .NotNull().WithMessage("Müşteri ID'si boş olamaz.")
            .GreaterThan(0).WithMessage("Müşteri ID'si sıfırdan büyük olmalıdır.");

        RuleFor(pr => pr.Rating)
            .NotNull().WithMessage("Puan boş olamaz.")
            .InclusiveBetween(1, 5).WithMessage("Puan 1 ile 5 arasında olmalıdır.");

        RuleFor(pr => pr.Comment)
            .MaximumLength(500).WithMessage("Yorum en fazla 500 karakter olabilir.");

        RuleFor(pr => pr.ReviewDate)
            .NotNull().WithMessage("İnceleme tarihi boş olamaz.");
    }
}
