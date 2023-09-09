using Domain.Tables;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class CustomTrouperValidator : IUserValidator<Trouper>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<Trouper> manager, Trouper user)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
