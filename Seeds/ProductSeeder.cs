﻿using itsc_dotnet_practice.Data;
using itsc_dotnet_practice.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Seeds;

public static class ProductSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient();

        // Only seed if Products table is empty
        if (context.Products.Any())
            return;

        var random = new Random();
        var products = new List<Product>();

        //var pokemonCount = await httpClient.GetAsync("https://pokeapi.co/api/v2/pokemon");

        for (int i = 1; i <= 10; i++)
        {
            try
            {
                var response = await httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{i}");
                if (!response.IsSuccessStatusCode)
                    continue;

                var jsonString = await response.Content.ReadAsStringAsync();
                using var jsonDoc = JsonDocument.Parse(jsonString);
                var root = jsonDoc.RootElement;

                var name = root.GetProperty("name").GetString() ?? $"pokemon-{i}";
                var imageUrl = root.GetProperty("sprites").GetProperty("front_default").GetString() ?? "";

                products.Add(new Product
                {
                    Name = name,
                    Description = $"A wild Pokémon: {name}",
                    Price = random.Next(10, 100),
                    Stock = random.Next(1, 50),
                    Category = "Pokémon",
                    ImageUrl = imageUrl,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to fetch Pokémon #{i}: {ex.Message}");
            }
        }

        if (products.Any())
        {
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
