using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CountryModel;

public partial class CountriesGoldenContext : DbContext
{
    public CountriesGoldenContext()
    {
    }

    public CountriesGoldenContext(DbContextOptions<CountriesGoldenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        var config = builder.Build();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__Table__F2D21B7683619915");

            entity.ToTable("City");

            entity.Property(e => e.Latitude)
                .HasColumnType("numeric(18, 4)")
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasColumnType("numeric(18, 4)")
                .HasColumnName("longitude");
            entity.Property(e => e.Name).HasDefaultValue("");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_City_Country");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Country__10D1609FC398FF50");

            entity.ToTable("Country");

            entity.Property(e => e.Iso2)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("iso2");
            entity.Property(e => e.Iso3)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("iso3");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
