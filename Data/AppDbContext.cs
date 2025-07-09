using Microsoft.EntityFrameworkCore;
using itsc_dotnet_practice.Models;

namespace itsc_dotnet_practice.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly map to lowercase PostgreSQL table name
            modelBuilder.Entity<User>().ToTable("users");

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
        }
    }
}