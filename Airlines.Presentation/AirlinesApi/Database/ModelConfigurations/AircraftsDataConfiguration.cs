using AirlinesApi.Database.DbContexts;
using AirlinesApi.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirlinesApi.Database.ModelConfigurations
{
    public class AircraftsDataConfiguration : IEntityTypeConfiguration<AircraftsData>
    {
        public void Configure(EntityTypeBuilder<AircraftsData> modelBuilder)
        {
            
            
                modelBuilder.HasKey(e => e.AircraftCode).HasName("aircrafts_pkey");

                modelBuilder.ToTable("aircrafts_data", AirlinesDbContext.DEFAULT_SCHEMA, tb => tb.HasComment("Aircrafts (internal data)"));

                modelBuilder.Property(e => e.AircraftCode)
                    .HasMaxLength(3)
                    .IsFixedLength()
                    .HasComment("Aircraft code, IATA")
                    .HasColumnName("aircraft_code");
                modelBuilder.Property(e => e.Model)
                    .HasComment("Aircraft model")
                    .HasColumnType("jsonb")
                    .HasColumnName("model");
                modelBuilder.Property(e => e.Range)
                    .HasComment("Maximal flying distance, km")
                    .HasColumnName("range");
            
            
        }
    }
}
