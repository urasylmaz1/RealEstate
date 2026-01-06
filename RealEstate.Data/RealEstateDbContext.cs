using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RealEstate.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace RealEstate.Data;

public class RealEstateDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : base(options)
    {
    }

    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyType> PropertyTypes { get; set; }
    public DbSet<PropertyImage> PropertyImages { get; set; }
    public DbSet<Inquiry> Inquiries { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Property>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<PropertyType>().HasQueryFilter(pt => !pt.IsDeleted);
        modelBuilder.Entity<PropertyImage>().HasQueryFilter(pi => !pi.IsDeleted);
        modelBuilder.Entity<Inquiry>().HasQueryFilter(i => !i.IsDeleted);

        // Seed data for PropertyTypes
        modelBuilder.Entity<PropertyType>().HasData(
            new PropertyType { Id = 1, Name = "Daire", Description = "Şehir içi konut", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow, IsDeleted = false },
            new PropertyType { Id = 2, Name = "Villa", Description = "Bağımsız konut", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow, IsDeleted = false },
            new PropertyType { Id = 3, Name = "Müstakil Ev", Description = "Tek başına ev", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow, IsDeleted = false },
            new PropertyType { Id = 4, Name = "Dükkan", Description = "Ticari işletme alanı", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow, IsDeleted = false },
            new PropertyType { Id = 5, Name = "Ofis", Description = "İşyeri ofisi", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow, IsDeleted = false },
            new PropertyType { Id = 6, Name = "Arsa", Description = "İnşaat arsası", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow, IsDeleted = false },
            new PropertyType { Id = 7, Name = "İşyeri", Description = "Genel ticari alan", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow, IsDeleted = false }
        );
    }
}