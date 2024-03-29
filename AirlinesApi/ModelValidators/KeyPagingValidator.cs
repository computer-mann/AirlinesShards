using AirlinesApi.ViewModels;
using FluentValidation;

namespace AirlinesApi.ModelValidators
{
    public class KeyPagingValidator:AbstractValidator<PaginationVm>
    {
        public KeyPagingValidator()
        { 
            //RuleFor(e=>e.Limit).
        }
    }
}
