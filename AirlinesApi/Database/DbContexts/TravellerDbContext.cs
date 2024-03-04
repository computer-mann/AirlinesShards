using AirlinesApi.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AirlinesApi.Database.DbContexts
{
    public class TravellerDbContext : IdentityDbContext<Traveller>
    {
        public TravellerDbContext(DbContextOptions<TravellerDbContext> options) : base(options)
        {

        }
      //  public DbSet<Traveller> Travellers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:Database");
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


            builder.Entity<Traveller>(entity =>
            {
                entity.ToTable("travellers");
                entity.Property(e => e.Id).HasColumnType("char(25)");
                entity.Property(e => e.PassengerName).HasColumnName("passenger_name").HasColumnType("varchar(75)").IsRequired();
                entity.Property(e => e.PhoneNumber).HasColumnType("varchar(15)");
                entity.Property(e => e.Country).HasColumnType("varchar(35)").IsRequired();
                entity.Property(e => e.UserName).HasColumnType("varchar(40)");
                entity.Property(e => e.NormalizedEmail).HasColumnType("varchar(40)");
                

            });

            builder.Entity<Traveller>().HasIndex(b=>b.NormalizedUserName).IsUnique().HasDatabaseName("idx_username");
        }
    }
}
