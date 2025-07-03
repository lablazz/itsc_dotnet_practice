using Microsoft.EntityFrameworkCore;
using itsc_dotnet_practice.Models;

namespace itsc_dotnet_practice.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<Product> Products { get; set; }
}
