using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tables
{
    internal class Staff:IdentityUser<int>
    {

        public override int Id { get; set; }
    }
}
