using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlinesApi.Database.Models
{
    internal class Staff : IdentityUser<int>
    {

        public override int Id { get; set; }
    }
}
