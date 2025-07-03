using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Repositories;

namespace itsc_dotnet_practice.Services;

public class ProductService
{
    private readonly ProductRepository _repo;

    public ProductService(ProductRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Product>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Product?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    public Task AddAsync(Product product) => _repo.AddAsync(product);
    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    public Task<bool> UpdateAsync(Product product) => _repo.UpdateAsync(product);
}
