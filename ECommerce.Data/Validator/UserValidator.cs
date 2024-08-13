using FluentValidation;
using ECommerce.Entities;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        // Kullanıcı adı boş olamaz.
        RuleFor(user => user.UserName)
            .NotEmpty()
            .WithMessage("Kullanıcı adı boş olamaz.");

        // E-posta adresi boş olamaz ve geçerli bir e-posta adresi olmalıdır.
        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Geçerli bir e-posta adresi giriniz.");

        // Şifre boş olamaz.
        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage("Şifre boş olamaz.");
    }
}
