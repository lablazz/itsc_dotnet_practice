using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.ModelDtos.ProductDto;

namespace itsc_dotnet_practice.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task AddAsync(Product product);

        Task<Product?> UpdateAsync(Product product);

        Task<bool> DeleteAsync(int id);
        Task<Product?> GetByNameAsync(string name);
    }
}