using itsc_dotnet_practice.Models;

namespace itsc_dotnet_practice.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);

        Task CreateProductAsync(Product product);

        Task<Product?> UpdateProductAsync(Product product);

        Task<bool> DeleteProductAsync(int id);
    }
}