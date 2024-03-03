using AirlinesApi.ViewModels;
using FluentValidation;

namespace AirlinesApi.ModelValidators
{
    public class LoginValidator:AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("'{PropertyValue}' is not a valid email address");
            RuleFor(x => x.Password).MinimumLength(6);
        }
    }
}
