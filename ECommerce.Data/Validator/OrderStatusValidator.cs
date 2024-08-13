using FluentValidation;
using ECommerce.DataAcces.Models;

namespace ECommerce.DataAcces.Validator
{
    public class OrderStatusValidator : AbstractValidator<OrderStatus>
    {
        public OrderStatusValidator()
        {
            // Durum açıklaması alanı için doğrulama kuralları
            RuleFor(os => os.StatusDescription)
                .NotEmpty().WithMessage("Durum açıklaması boş olamaz.") // Boş olmamalı
                .Length(1, 255).WithMessage("Durum açıklaması 1 ile 255 karakter arasında olmalıdır."); // 1-255 karakter uzunluğunda olmalı

            // Tarih alanları için doğrulama kuralları
            RuleFor(os => os.CreatedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Oluşturulma tarihi bugünden ileri bir tarih olamaz."); // Gelecek bir tarih olmamalı

            RuleFor(os => os.UpdatedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Güncellenme tarihi bugünden ileri bir tarih olamaz."); // Gelecek bir tarih olmamalı

            RuleFor(os => os.DeletedDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Silinme tarihi bugünden ileri bir tarih olamaz."); // Gelecek bir tarih olmamalı

            // Boolean alanlar için doğrulama kuralları
            RuleFor(os => os.IsDeleted)
                .NotNull().WithMessage("Silinme durumu boş olamaz."); // Boş olmamalı

            RuleFor(os => os.IsActive)
                .NotNull().WithMessage("Aktiflik durumu boş olamaz."); // Boş olmamalı
        }
    }
}
