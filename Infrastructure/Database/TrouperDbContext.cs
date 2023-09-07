
using Domain.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class TrouperDbContext : IdentityDbContext<Trouper>
    {
        public TrouperDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(AirlinesDbContext.DEFAULT_SCHEMA);
            builder.Entity<IdentityRole>().ToTable(options => options.ExcludeFromMigrations());
            builder.Entity<IdentityUserLogin>().ToTable(options => options.ExcludeFromMigrations());
            

            builder.Entity<Trouper>(entity =>
            {
                entity.ToTable("troupers");
                entity.Property(e => e.Id).HasColumnType("char(25)");
                entity.Property(e => e.PassengerName).HasColumnName("passenger_name").HasColumnType("text").IsRequired();
                entity.Property(e => e.PhoneNumber).HasColumnType("varchar(15)");
                entity.Property(e => e.Country).HasColumnType("varchar(35)").IsRequired();
            });
        }
    }
}
