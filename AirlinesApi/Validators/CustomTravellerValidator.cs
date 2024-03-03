using AirlinesApi.Database.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Validators
{

    public class CustomTravellerValidator : IUserValidator<Traveller>
    {
        public IdentityErrorDescriber Describer { get; private set; }
        public CustomTravellerValidator(IdentityErrorDescriber? errors = null)
        {
            Describer = errors ?? new IdentityErrorDescriber();
        }
        public async Task<IdentityResult> ValidateAsync(UserManager<Traveller> manager, Traveller user)
        {
            ArgumentNullException.ThrowIfNull(manager);
            ArgumentNullException.ThrowIfNull(user);
            List<IdentityError>? errors = new List<IdentityError>();
            if (manager.Options.User.RequireUniqueEmail)
            {
                errors = await ValidateEmailAsync(manager, user, errors).ConfigureAwait(false);
            }
            return errors?.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
        }
        private async Task<List<IdentityError>?> ValidateEmailAsync(UserManager<Traveller> manager, Traveller user, List<IdentityError>? errors)
        {
            var email = await manager.GetEmailAsync(user).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(email))
            {
                errors ??= new List<IdentityError>();
                errors.Add(Describer.InvalidEmail(email));
                return errors;
            }
            if (!new EmailAddressAttribute().IsValid(email))
            {
                errors ??= new List<IdentityError>();
                errors.Add(Describer.InvalidEmail(email));
                return errors;
            }
            var owner = await manager.FindByEmailAsync(email).ConfigureAwait(false);
            if (owner != null &&
                !string.Equals(await manager.GetUserIdAsync(owner).ConfigureAwait(false), await manager.GetUserIdAsync(user).ConfigureAwait(false)))
            {
                errors ??= new List<IdentityError>();
                errors.Add(Describer.DuplicateEmail(email));
            }
            return errors;
        }
    }
}
