using AirlinesApi.ViewModels;
using FluentValidation;

namespace AirlinesApi.ModelValidators
{
    public class KeyPagingValidator:AbstractValidator<KeyPaging>
    {
        public KeyPagingValidator()
        { 
            //RuleFor(e=>e.Limit).
        }
    }
}
