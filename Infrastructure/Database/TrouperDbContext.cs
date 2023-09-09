﻿
using Domain.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class TrouperDbContext : IdentityDbContext<Trouper>
    {
        public TrouperDbContext(DbContextOptions<TrouperDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(AirlinesDbContext.DEFAULT_SCHEMA);
            builder.Entity<IdentityRole>().ToTable(options => options.ExcludeFromMigrations());
            builder.Entity<IdentityUserLogin<string>>().ToTable(options => options.ExcludeFromMigrations());
            builder.Entity<IdentityUserRole<string>>().ToTable(options => options.ExcludeFromMigrations());
            builder.Entity<IdentityUserClaim<string>>().ToTable(options => options.ExcludeFromMigrations());
            builder.Entity<IdentityUserToken<string>>().ToTable(options => options.ExcludeFromMigrations());
            builder.Entity<IdentityRoleClaim<string>>().ToTable(options => options.ExcludeFromMigrations());
           

            builder.Entity<Trouper>(entity =>
            {
                entity.ToTable("troupers");
                entity.Property(e => e.Id).HasColumnType("char(25)");
                entity.Property(e => e.PassengerName).HasColumnName("passenger_name").HasColumnType("varchar(75)").IsRequired();
                entity.Property(e => e.PhoneNumber).HasColumnType("varchar(15)");
                entity.Property(e => e.Country).HasColumnType("varchar(35)").IsRequired();
                //entity.Property(e=>e.UserName).Ig  

            });
            builder.Entity<Trouper>().Ignore(x => x.UserName);
            builder.Entity<Trouper>().Ignore(x => x.NormalizedUserName);
            builder.Entity<Trouper>().Ignore(x => x.ConcurrencyStamp);
        }
    }
}
