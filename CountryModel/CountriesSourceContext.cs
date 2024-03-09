using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CountryModel;

public partial class CountriesSourceContext : DbContext
{
    public CountriesSourceContext()
    {
    }

    public CountriesSourceContext(DbContextOptions<CountriesSourceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appserrings.json");
        var config = builder.Build();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    }
#warning 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__Table__F2D21B7683619915");

            entity.Property(e => e.CityId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.CityNavigation).WithOne(p => p.City)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_City_Country");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Country__10D1609FC398FF50");

            entity.Property(e => e.Iso2).IsFixedLength();
            entity.Property(e => e.Iso3).IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
