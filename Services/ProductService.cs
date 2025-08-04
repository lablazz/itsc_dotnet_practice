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

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _repo.GetAllProducts();
    }
    public async Task<Product?> GetProductById(int id)
    {
        return await _repo.GetProductById(id);
    }
    public async Task<List<Product>> GetProductByQuery(string query)
    {
        return await _repo.GetProductByQuery(query);
    }
    public async Task<Product> CreateProduct(ProductDto.Request productDto)
    {
        return await _repo.CreateProduct(productDto);
    }
    public async Task<Product> UpdateProduct(int id, Product productDto)
    {
        return await _repo.UpdateProduct(id, productDto);
    }
    public async Task<bool> DeleteProduct(int id)
    {
        return await _repo.DeleteProduct(id);
    }
}