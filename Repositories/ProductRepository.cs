using itsc_dotnet_practice.Data;
using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using itsc_dotnet_practice.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }
    public async Task<Product> CreateProductAsync(ProductDto.Request product)
    {
        var newProduct = new Product
        {
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Stock = product.Stock,
            Category = product.Category,
            ImageUrl = product.ImageUrl
        };
        _context.Products.Add(newProduct);
        _context.SaveChanges();
        return newProduct;
    }
    public async Task<Product> UpdateProductAsync(int id, Product product)
    {
        var existingProduct = await _context.Products.FindAsync(id);
        if (existingProduct == null) return null;
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.Description = product.Description;
        _context.Products.Update(existingProduct);
        _context.SaveChanges();
        return existingProduct;
    }
    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;
        _context.Products.Remove(product);
        _context.SaveChanges();
        return true;
    }
}