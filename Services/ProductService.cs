using itsc_dotnet_practice.Services.Interface;
using itsc_dotnet_practice.Repositories.Interface;
using itsc_dotnet_practice.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using itsc_dotnet_practice.Models.Dtos;
using itsc_dotnet_practice.Models;

namespace itsc_dotnet_practice.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return (IEnumerable<Product>) await _repo.GetAllProductsAsync();
    }
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _repo.GetProductByIdAsync(id);
    }
    public async Task<Product> CreateProductAsync(ProductDto.Request productDto)
    {
        return await _repo.CreateProductAsync(productDto);
    }
    public async Task<Product> UpdateProductAsync(int id, Product productDto)
    {
        return await _repo.UpdateProductAsync(id, productDto);
    }
    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _repo.DeleteProductAsync(id);
    }
}