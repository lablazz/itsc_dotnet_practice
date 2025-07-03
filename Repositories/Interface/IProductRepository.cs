using itsc_dotnet_practice.Models;

namespace itsc_dotnet_practice.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task<Product?> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}
