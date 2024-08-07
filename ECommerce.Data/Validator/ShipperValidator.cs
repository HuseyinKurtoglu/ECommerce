using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ECommerce.DataAcces.Models;

namespace ECommerce.DataAcces.Validator
{


    namespace ECommerce.DataAcces.Validator
    {
        public class ShipperValidator : AbstractValidator<Shipper>
        {
            public ShipperValidator()
            {
                // Şirket adı alanı için doğrulama kuralları
                RuleFor(s => s.CompanyName)
                    .NotEmpty().WithMessage("Şirket adı boş olamaz.") // Boş olmamalı
                    .Length(1, 100).WithMessage("Şirket adı 1 ile 100 karakter arasında olmalıdır."); // 1-100 karakter uzunluğunda olmalı

                // Telefon numarası alanı için doğrulama kuralları
                RuleFor(s => s.Phone)
                    .NotEmpty().WithMessage("Telefon numarası boş olamaz.") // Boş olmamalı
                    .Matches(@"^\+?\d{10,15}$").WithMessage("Geçersiz telefon numarası."); // 10-15 haneli numara, isteğe bağlı olarak "+" ile başlayabilir

            }
        }
    }

}
