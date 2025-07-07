using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.ModelDtos.ProductDto;

namespace itsc_dotnet_practice.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);

        Task<Product> CreateProductAsync(ProductModelRequest request);

        Task<Product?> UpdateProductAsync(int id, ProductModelRequest product);

        Task<bool> DeleteProductAsync(int id);
    }
}