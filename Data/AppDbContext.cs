using itsc_dotnet_practice.Models;
using Microsoft.EntityFrameworkCore;

namespace itsc_dotnet_practice.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Order → OrderItems
        modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Order → User
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany() // or WithMany(u => u.Orders) if you want reverse navigation
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict); // or your preferred behavior
    }

}
