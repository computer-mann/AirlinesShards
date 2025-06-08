using AirlinesApi.ViewModels;
using FluentValidation;

namespace AirlinesApi.ModelValidators
{
    public class LoginValidator:AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(4);
        }
    }
}
