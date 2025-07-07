using itsc_dotnet_practice.Data;
using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.ModelDtos.UserDto;
using itsc_dotnet_practice.Services;
using Microsoft.EntityFrameworkCore;

namespace itsc_dotnet_practice.Seeds;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(AppDbContext context, UserService userService)
    {
        if (await context.Users.AnyAsync()) // Already seeded
            return;

        // Seed Users via DTO with ConfirmPassword
        var user1Dto = new CreateUserDtoRequest
        {
            Email = "alice@example.com",
            FirstName = "Alice",
            LastName = "Smith",
            Password = "password123",
            ConfirmPassword = "password123",
            Phone = "1234567890"
        };

        var user2Dto = new CreateUserDtoRequest
        {
            Email = "bob@example.com",
            FirstName = "Bob",
            LastName = "Johnson",
            Password = "password456",
            ConfirmPassword = "password456",
            Phone = "0987654321"
        };

        await userService.CreateUserAsync(user1Dto);
        await userService.CreateUserAsync(user2Dto);

        // Reload users with encrypted phones and hashed passwords
        var alice = await context.Users.FirstAsync(u => u.Email == user1Dto.Email);

        // Seed Products
        var product1 = new Product
        {
            Name = "Laptop",
            Description = "High performance laptop",
            Price = 1500.00m,
            Stock = 10
        };

        var product2 = new Product
        {
            Name = "Headphones",
            Description = "Noise cancelling headphones",
            Price = 200.00m,
            Stock = 30
        };

        await context.Products.AddRangeAsync(product1, product2);
        await context.SaveChangesAsync();

        // Seed Order for Alice
        var order1 = new Order
        {
            UserId = alice.Id,
            OrderDate = DateTime.Now.AddDays(-10),
            TotalAmount = 1700m,
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductName = product1.Name,
                    Quantity = 1,
                    Price = product1.Price
                },
                new OrderItem
                {
                    ProductName = product2.Name,
                    Quantity = 1,
                    Price = product2.Price
                }
            }
        };

        await context.Orders.AddAsync(order1);
        await context.SaveChangesAsync();
    }
}
