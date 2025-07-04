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
}