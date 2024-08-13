using FluentValidation;
using ECommerce.DataAcces.Models;

namespace ECommerce.DataAcces.Validator
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            // İlk isim alanı için doğrulama kuralları
            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("İsim boş olamaz.") // Boş olmamalı
                .Length(1, 50).WithMessage("İsim 1 ile 50 karakter arasında olmalıdır."); // 1-50 karakter uzunluğunda olmalı

            // Soyisim alanı için doğrulama kuralları
            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Soyisim boş olamaz.") // Boş olmamalı
                .Length(1, 50).WithMessage("Soyisim 1 ile 50 karakter arasında olmalıdır."); // 1-50 karakter uzunluğunda olmalı

            // E-posta adresi alanı için doğrulama kuralları
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.") // Boş olmamalı
                .EmailAddress().WithMessage("Geçersiz e-posta adresi."); // Geçerli bir e-posta adresi formatında olmalı

            // Telefon numarası alanı için doğrulama kuralları
            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.") // Boş olmamalı
                .Matches(@"^\+?\d{10,15}$").WithMessage("Geçersiz telefon numarası."); // 10-15 haneli numara, isteğe bağlı olarak "+" ile başlayabilir
        }
    }
}
