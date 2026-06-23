using Microsoft.EntityFrameworkCore;
using ShopSystem.Models;

namespace ShopSystem.Data;

public class ShopDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasConversion<string>();
            entity.ToTable("Orders", "dbo");
        });
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Description).IsUnicode();
            entity.ToTable("Products", "dbo");
        });
        
    }
}