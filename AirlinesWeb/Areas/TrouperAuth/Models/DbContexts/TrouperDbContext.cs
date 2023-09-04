using AirlinesWeb.Areas.PilgrimAuth.Models;
using AirlinesWeb.Models.DbContexts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AirlinesWeb.Areas.TrouperAuth.Models.DbContexts
{
    public class TrouperDbContext:IdentityDbContext<Trouper>
    {
        public TrouperDbContext(DbContextOptions options) :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(AirlinesContext.DEFAULT_SCHEMA);

            builder.Entity<Trouper>(entity =>
            {
                entity.ToTable("troupers");
                entity.Property(e => e.Id).HasColumnType("char(25)");
                entity.Property(e => e.PassengerName).HasColumnName("passenger_name").HasColumnType("text");
                entity.Property(e => e.PhoneNumber).HasColumnType("varchar(15)");
                entity.Property(e => e.Country).HasColumnType("varchar(35)");
            });
        }
    }
}
