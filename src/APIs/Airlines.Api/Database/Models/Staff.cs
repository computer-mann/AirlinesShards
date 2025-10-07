using Microsoft.AspNetCore.Identity;

namespace AirlinesApi.Database.Models
{
    internal class Staff : IdentityUser<int>
    {

        public override int Id { get; set; }
    }
}
