using AirlinesWeb.Areas.PilgrimAuth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AirlinesWeb.Areas.TrouperAuth.Models.DbContexts
{
    public class TrouperDbContext:IdentityDbContext<Trouper>
    {
        public TrouperDbContext(DbContextOptions options) :base(options)
        {
            
        }
    }
}
