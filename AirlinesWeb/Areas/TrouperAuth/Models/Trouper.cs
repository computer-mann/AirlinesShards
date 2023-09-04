using Microsoft.AspNetCore.Identity;

namespace AirlinesWeb.Areas.PilgrimAuth.Models
{
    public class Trouper : IdentityUser
    {
        public Trouper() 
        {
            Id = UuidExtensions.Uuid7.Id25();
            SecurityStamp = Guid.NewGuid().ToString();
        }
    }
}
