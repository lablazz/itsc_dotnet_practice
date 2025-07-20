using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using itsc_dotnet_practice.Data;
using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Utilities;

namespace itsc_dotnet_practice.Seeds
{
    public static class UserSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // If there are already users, skip seeding
            if (context.Users.Any())
            {
                Console.WriteLine("Users already exist, skipping seed.");
                return;
            }

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Seeds", "DataJson", "mock_users.json");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Seed file not found: {filePath}");
                return;
            }

            var json = File.ReadAllText(filePath);
            var users = JsonSerializer.Deserialize<List<User>>(json);

            if (users != null && users.Count > 0)
            {
                foreach (var user in users)
                {
                    user.Password = EncryptionUtility.HashPassword(user.Password);
                }

                context.Users.AddRange(users);
                context.SaveChanges();

                Console.WriteLine($"Seeded {users.Count} users successfully!");
            }
            else
            {
                Console.WriteLine("No users found in seed file.");
            }
        }
    }
}
