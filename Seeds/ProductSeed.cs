using itsc_dotnet_practice.Models;
using Microsoft.EntityFrameworkCore;

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
                    Name = "Handmade Karen Shirt",
                    Description = "Unique handwoven Karen shirt with traditional patterns.",
                    Price = 59.99M,
                    Stock = 20,
                    ImageUrl = "https://example.com/images/karen-shirt.jpg",
                    Category = "Clothing"
                },
                new Product
                {
                    Id = 2,
                    Name = "Embroidered Scarf",
                    Description = "Soft scarf with beautiful hand embroidery.",
                    Price = 29.99M,
                    Stock = 50,
                    ImageUrl = "https://example.com/images/scarf.jpg",
                    Category = "Accessories"
                }
                // Add more products here if you like
            );
        }
    }
}
