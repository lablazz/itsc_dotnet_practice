using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace itsc_dotnet_practice.Services.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(ProductDto.Request productDto);
        Task<Product> UpdateProductAsync(int id, Product productDto);
        Task<bool> DeleteProductAsync(int id);
    }
}
