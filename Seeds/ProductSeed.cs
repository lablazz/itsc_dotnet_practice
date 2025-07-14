using itsc_dotnet_practice.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace itsc_dotnet_practice.Seeds
{
    public static class ProductSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Handwoven Karen Shirt",
                    Description = "Beautiful handmade Karen shirt with traditional patterns.",
                    Price = 49.99m,
                    Stock = 25,
                    Category = "Clothing",
                    ImageUrl = "https://example.com/images/karen-shirt.jpg",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 2,
                    Name = "Embroidered Cotton Bag",
                    Description = "Natural cotton bag with traditional Karen embroidery.",
                    Price = 19.99m,
                    Stock = 40,
                    Category = "Accessories",
                    ImageUrl = "https://example.com/images/cotton-bag.jpg",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 3,
                    Name = "Handmade Scarf",
                    Description = "Soft scarf with intricate hand embroidery, perfect for any season.",
                    Price = 29.99m,
                    Stock = 30,
                    Category = "Accessories",
                    ImageUrl = "https://example.com/images/scarf.jpg",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
