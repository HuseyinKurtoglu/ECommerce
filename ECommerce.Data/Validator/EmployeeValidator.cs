using ECommerce.DataAcces.Models;
using FluentValidation;

namespace ECommerce.Business.ValidationRules.FluentValidation
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("İsim boş olamaz.")
                .MaximumLength(100).WithMessage("İsim en fazla 100 karakter olabilir.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyisim boş olamaz.")
                .MaximumLength(100).WithMessage("Soyisim en fazla 100 karakter olabilir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(150).WithMessage("E-posta en fazla 150 karakter olabilir.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz.");

            RuleFor(x => x.JobTitle)
                .NotEmpty().WithMessage("İş unvanı boş olamaz.")
                .MaximumLength(100).WithMessage("İş unvanı en fazla 100 karakter olabilir.");
        }
    }
}
