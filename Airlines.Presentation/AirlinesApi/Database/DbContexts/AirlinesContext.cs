using System;
using System.Collections.Generic;
using AirlinesApi.Database.Base_Models;
using Microsoft.EntityFrameworkCore;

namespace AirlinesApi.Database.DbContexts;

public partial class AirlinesContext : DbContext
{
    public AirlinesContext()
    {
    }

    public AirlinesContext(DbContextOptions<AirlinesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Traveller> Travellers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:Database");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Traveller>(entity =>
        {
            entity.ToTable("Travellers", "bookings");

            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "idx_username").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(25)
                .IsFixedLength();
            entity.Property(e => e.Country).HasMaxLength(35);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(40);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.PassengerName)
                .HasMaxLength(75)
                .HasColumnName("passenger_name");
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.UserName).HasMaxLength(40);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
