using BCrypt.Net;
using itsc_dotnet_practice.Models;
using System;
using System.Linq;

namespace itsc_dotnet_practice.Data
{
    public static class DbSeeder
    {
        public static void SeedAdminUser(AppDbContext context)
        {
            if (!context.Users.Any(u => u.Username == "admin"))
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword("secret123");

                var admin = new User
                {
                    Username = "admin",
                    Password = hashedPassword,
                    Role = "Admin"
                };

                context.Users.Add(admin);
                context.SaveChanges();
                Console.WriteLine("✅ Admin user seeded.");
            }
            else
            {
                Console.WriteLine("ℹ️ Admin user already exists.");
            }
        }
    }
}
